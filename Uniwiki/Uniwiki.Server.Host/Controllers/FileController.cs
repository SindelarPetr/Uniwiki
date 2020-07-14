using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Server.Appliaction.ServerActions;
using Server.Appliaction.Services.Abstractions;
using Shared;
using Shared.Dtos;
using Shared.Exceptions;
using Uniwiki.Client.Host;
using Uniwiki.Server.Host.Mvc;
using Uniwiki.Server.Host.Services;
using Uniwiki.Server.Host.Services.Abstractions;
using Uniwiki.Shared;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Host.Controllers
{
    [Route(ApiRoutes.FileController.FileControllerRoute)]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMvcProcessor _mvcProcessor;
        private readonly IMvcRequestExceptionHandlerService _mvcRequestExceptionHandlerService;
        private readonly IRequestDeserializer _requestDeserializer;
        private readonly IUploadFileService _uploadFileService;
        private readonly ILogger<FileController> _logger;
        private readonly TextService _textService;
        private readonly InputContextService _inputContextService;

        public FileController(IMvcProcessor mvcProcessor, IMvcRequestExceptionHandlerService mvcRequestExceptionHandlerService, IRequestDeserializer requestDeserializer, IUploadFileService uploadFileService, ILogger<FileController> logger, TextService textService, InputContextService inputContextService)
        {
            _mvcProcessor = mvcProcessor;
            _mvcRequestExceptionHandlerService = mvcRequestExceptionHandlerService;
            _requestDeserializer = requestDeserializer;
            _uploadFileService = uploadFileService;
            _logger = logger;
            _textService = textService;
            _inputContextService = inputContextService;
        }


        [HttpPost]
        [RequestSizeLimit(Constants.MaxFileSizeInBytes)]
        [RequestFormLimits(MultipartBodyLengthLimit = Constants.MaxFileSizeInBytes)]
        public async Task<ActionResult> Post()
        {
            _logger.LogInformation("Received a file upload request");
            
            // Chack if there are some files
            if (HttpContext.Request.Form.Files.Count == 0)
            {
                _logger.LogWarning("The received file upload didnt contain any files!");
                return BadRequest(_textService.Error_NoFilesReceived);
            }

            // Check if there is exactly one file
            if (HttpContext.Request.Form.Files.Count > 1)
            {
                _logger.LogWarning("The received file upload contains too many files (FilesCount)!", HttpContext.Request.Form.Files.Count);
                return BadRequest(_textService.Error_TooManyFiles);
            }

            // Check if the form contains the field 'Data'
            if (!HttpContext.Request.Form.ContainsKey("Data"))
            {
                _logger.LogWarning("Tried to upload a file without filling in the data for server");
                return BadRequest(_textService.Error_DataFieldIsMissing);
            }

            try
            {
                // Get the file
                var file = HttpContext.Request.Form.Files.First();

                // Prepare the file to be served to a server action
                _uploadFileService.RegisterFile(file);

                // Get the data for server
                var serializedDataForServer = HttpContext.Request.Form["Data"].First();
                
                // Deserialize the data for server
                _logger.LogInformation("Deserializing the data for server");
                var dataForServer = JsonConvert.DeserializeObject<DataForServer>(serializedDataForServer);

                // Check if serialization succeeded
                if (dataForServer == null)
                {
                    _logger.LogWarning("Could not deserialize the data for server");
                    return BadRequest(_textService.Error_CouldNotDeserializeRequest); 
                }

                // Deserialize the request
                _logger.LogInformation("Deserializing the request");
                var request = _requestDeserializer.Deserialize(dataForServer.Request, dataForServer.Type);

                // Get the input context
                var inputContext = _inputContextService.CreateFromHttpContext(dataForServer.AccessToken, dataForServer.Language, ClientConstants.AppVersionString, HttpContext);

                // Process the request
                _logger.LogInformation("Processing the upload request");
                var response = await _mvcProcessor.Process(request, inputContext);
                _logger.LogInformation("Successfully processed the upload request");

                return new JsonResult(response);
            }
            catch (RequestException e)
            {
                return _mvcRequestExceptionHandlerService.HandleRequestException(e, this);
            }
            catch (Exception e)
            {
                return _mvcRequestExceptionHandlerService.HandleException(e, this);
            }

        }

        [HttpGet(nameof(GetPostFile))]
        public async Task<ActionResult> GetPostFile(
            [FromQuery(Name = ApiRoutes.FileController.FileIdParameter)] Guid id, 
            [FromQuery(Name = ApiRoutes.FileController.SecondaryTokenParameter)] Guid accessToken, 
            [FromQuery(Name = ApiRoutes.FileController.FileNameParameter)] string fileName, 
            [FromQuery(Name = ApiRoutes.FileController.LanguageParameter)] Language language)
        {
            // Validate the recived parameters
            if (id == default || accessToken == default || fileName == default || language == default)
            {
                // Log failure
                _logger.LogWarning("Could not download a file, because some of the arguments are missing. File id: '{FileId}', Has Valid Token: {HasValidToken}, File name: '{FileName}', Language: '{Language}'", id, accessToken != default, fileName, language);
                return Redirect(GetBaseUri(HttpContext) + PageRoutes.DownloadErrorPage.BuildRoute()); 
            }

            // Create input context
            InputContext inputContext = _inputContextService.CreateFromHttpContext(accessToken, language, ClientConstants.AppVersionString, HttpContext);

            // Decode the file name from URL format
            var decodedFileName = HttpUtility.UrlDecode(fileName);

            // Create the request for the file
            var request = new GetPostFileRequest(id, decodedFileName);

            try
            {
                // Log information about request to download
                _logger.LogInformation("Going to download a file. File id: '{FileId}', File name: '{FileName}', Language: '{Language}'", id, decodedFileName, language);

                // Process the request
                var response = await _mvcProcessor.Process(request, inputContext);

                // Check if was able to generate a response
                if (response.Response == null)
                {
                    _logger.LogWarning("The processor produced an empty response when attempting to download a file. File id: '{FileId}', File name: '{FileName}", id, decodedFileName);
                    return Redirect(GetBaseUri(HttpContext) + PageRoutes.DownloadErrorPage.BuildRoute());
                }

                // Check if generated the right response
                if (!(response.Response is GetPostFileResponse getPostFileResponse))
                {
                    _logger.LogWarning("The processor produced a wrong response (produced '{ResponseType}') when attempting to download a file. File id: '{FileId}', File name: '{FileName}", response.Response?.GetType(), id, decodedFileName);
                    return Redirect(GetBaseUri(HttpContext) + PageRoutes.DownloadErrorPage.BuildRoute());
                }

                // Return the file
                return File(getPostFileResponse.FileStream, System.Net.Mime.MediaTypeNames.Application.Octet,
                    getPostFileResponse.OriginalName);
            }
            catch (FileNotFoundException e)
            {
                _logger.LogWarning(e, "Was not able to find the file. File id: '{FileId}', File name: '{FileName}", id, decodedFileName);
                return Redirect(GetBaseUri(HttpContext) + PageRoutes.FileNotFoundErrorPage.BuildRoute());
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Was not able to find the file. File id: '{FileId}', File name: '{FileName}", id, decodedFileName);
                return Redirect(GetBaseUri(HttpContext) + PageRoutes.DownloadErrorPage.BuildRoute());
            }
        }

        private string GetBaseUri(HttpContext context)
        {
            // If there is not context, then dont care about it
            if (context == null)
            {
                return string.Empty;
            }

            var request = context.Request;

            var host = request.Host.ToUriComponent();

            var pathBase = request.PathBase.ToUriComponent();

            return $"{request.Scheme}://{host}{pathBase}";
        }
    }
}

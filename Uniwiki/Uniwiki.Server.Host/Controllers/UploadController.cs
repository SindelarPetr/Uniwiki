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
using Uniwiki.Server.Host.Services.Abstractions;
using Uniwiki.Shared;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Host.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IMvcProcessor _mvcProcessor;
        private readonly IMvcRequestExceptionHandlerService _mvcRequestExceptionHandlerService;
        private readonly IRequestDeserializer _requestDeserializer;
        private readonly IUploadFileService _uploadFileService;
        private readonly ILogger<UploadController> _logger;

        public UploadController(IMvcProcessor mvcProcessor, IMvcRequestExceptionHandlerService mvcRequestExceptionHandlerService, IRequestDeserializer requestDeserializer, IUploadFileService uploadFileService, ILogger<UploadController> logger)
        {
            _mvcProcessor = mvcProcessor;
            _mvcRequestExceptionHandlerService = mvcRequestExceptionHandlerService;
            _requestDeserializer = requestDeserializer;
            _uploadFileService = uploadFileService;
            _logger = logger;
        }


        [HttpPost]
        [RequestSizeLimit(Constants.MaxFileSizeInBytes)]
        [RequestFormLimits(MultipartBodyLengthLimit = Constants.MaxFileSizeInBytes)]
        public async Task<ActionResult> Post()
        {
            _logger.LogInformation("Received a file upload request");
            if (HttpContext.Request.Form.Files.Count == 0)
            {
                _logger.LogWarning("The received file upload didnt contain any files!");
                return BadRequest("Didnt receive any files :(");
            }

            if (HttpContext.Request.Form.Files.Count > 1)
            {
                _logger.LogWarning("The received file upload contains too many files (FilesCount)!", HttpContext.Request.Form.Files.Count);
                return BadRequest("Received too many files. Send just one file at a time.");
            }

            try
            {
                // Get the file
                var file = HttpContext.Request.Form.Files.First();

                // Prepare the file to be served to a server action
                _uploadFileService.RegisterFile(file);

                //// TODO: Catch exception when something messes up (like when user cancels it, or connections gets lost, or there is not enough disk space)

                if (!HttpContext.Request.Form.ContainsKey("Data"))
                {
                    _logger.LogWarning("Tryed to upload a file without filling in the data for server");
                    return BadRequest("The request must contain " + Constants.FileUploadDataField +
                               " form field with DataForServer"); // TODO: Translate
                }

                var serializedDataForServer = HttpContext.Request.Form["Data"].First();
                
                _logger.LogInformation("Deserializing the data for server");
                var dataForServer = JsonConvert.DeserializeObject<DataForServer>(serializedDataForServer);

                if (dataForServer == null)
                {
                    _logger.LogWarning("Could not deserialize the data for server");
                    return BadRequest($"Couldnt deserialize '{serializedDataForServer}' to DataForServer"); 
                }

                var inputContext = new InputContext(dataForServer.AccessToken, Guid.NewGuid().ToString(), dataForServer.Language, ClientConstants.AppVersionString);

                _logger.LogInformation("Deserializing the request");
                var request = _requestDeserializer.Deserialize(dataForServer.Request, dataForServer.Type);

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
            [FromQuery(Name = ApiRoutes.UploadController.FileIdParameter)] Guid id, 
            [FromQuery(Name = ApiRoutes.UploadController.SecondaryTokenParameter)] Guid accessToken, 
            [FromQuery(Name = ApiRoutes.UploadController.FileNameParameter)] string fileName, 
            [FromQuery(Name = ApiRoutes.UploadController.LanguageParameter)] Language language)
        {
            // Validate the recived parameters
            if (id == default || accessToken == default || fileName == default || language == default)
            {
                // Log failure
                _logger.LogWarning("Could not download a file, because some of the arguments are missing. File id: '{FileId}', Has Valid Token: {HasValidToken}, File name: '{FileName}', Language: '{Language}'", id, accessToken != default, fileName, language);
                return Redirect(GetBaseUri(HttpContext) + PageRoutes.DownloadErrorPage.BuildRoute()); 
            }

            // Create input context
            InputContext inputContext = new InputContext(accessToken, HttpContext.TraceIdentifier, language, ClientConstants.AppVersionString);

            // Decode the file name from URL format
            var decodedFileName = HttpUtility.UrlDecode(fileName);

            var request = new GetPostFileRequest(id, decodedFileName);

            try
            {
                // Log information about request to download
                _logger.LogInformation("Going to download a file. File id: '{FileId}', File name: '{FileName}', Language: '{Language}'", id, decodedFileName, language);

                var response = await _mvcProcessor.Process(request, inputContext);

                if (response.Response == null)
                {
                    _logger.LogWarning("The processor produced an empty response when attempting to download a file. File id: '{FileId}', File name: '{FileName}", id, decodedFileName);
                    return Redirect(GetBaseUri(HttpContext) + PageRoutes.DownloadErrorPage.BuildRoute());
                }

                if (!(response.Response is GetPostFileResponse getPostFileResponse))
                {
                    _logger.LogWarning("The processor produced a wrong response (produced '{ResponseType}') when attempting to download a file. File id: '{FileId}', File name: '{FileName}", response.Response?.GetType(), id, decodedFileName);
                    return Redirect(GetBaseUri(HttpContext) + PageRoutes.DownloadErrorPage.BuildRoute());
                }

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

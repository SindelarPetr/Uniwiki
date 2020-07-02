using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public UploadController(IMvcProcessor mvcProcessor, IMvcRequestExceptionHandlerService mvcRequestExceptionHandlerService, IRequestDeserializer requestDeserializer, IUploadFileService uploadFileService)
        {
            _mvcProcessor = mvcProcessor;
            _mvcRequestExceptionHandlerService = mvcRequestExceptionHandlerService;
            _requestDeserializer = requestDeserializer;
            _uploadFileService = uploadFileService;
        }


        [HttpPost]
        [RequestSizeLimit(Constants.MaxFileSizeInBytes)]
        [RequestFormLimits(MultipartBodyLengthLimit = Constants.MaxFileSizeInBytes)]
        public async Task<ActionResult> Post()
        {

            if (HttpContext.Request.Form.Files.Count == 0)
            {
                Console.WriteLine("Didnt receive any files :(");
                return BadRequest("Didnt receive any files :(");
            }

            if (HttpContext.Request.Form.Files.Count > 1)
            {
                Console.WriteLine("Received too many files. Send just one file at a time.");
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
                    return BadRequest("The request must contain " + Constants.FileUploadDataField +
                               " form field with DataForServer"); // TODO: Translate

                var serializedDataForServer = HttpContext.Request.Form["Data"].First();

                var dataForServer = JsonConvert.DeserializeObject<DataForServer>(serializedDataForServer);

                if (dataForServer == null)
                    return BadRequest($"Couldnt deserialize '{serializedDataForServer}' to DataForServer");

                var inputContext = new InputContext(dataForServer.AccessToken, Guid.NewGuid(), dataForServer.Language, ClientConstants.AppVersionString);

                var request = _requestDeserializer.Deserialize(dataForServer.Request, dataForServer.Type);

                var response = await _mvcProcessor.Process(request, inputContext);

                return new JsonResult(response);
            }
            catch (RequestException e)
            {
                return _mvcRequestExceptionHandlerService.HandleRequestException(e, this);
            }
            catch (Exception)
            {
                return _mvcRequestExceptionHandlerService.HandleServerException(this);
            }

        }

        [HttpGet(nameof(GetPostFile))]
        public async Task<ActionResult> GetPostFile(
            [FromQuery(Name = ApiRoutes.UploadController.FileIdParameter)] Guid id, 
            [FromQuery(Name = ApiRoutes.UploadController.SecondaryTokenParameter)] Guid accessToken, 
            [FromQuery(Name = ApiRoutes.UploadController.FileNameParameter)] string fileName, 
            [FromQuery(Name = ApiRoutes.UploadController.LanguageParameter)] Language language)
        {
            if (id == default || accessToken == default || fileName == default || language == default)
                return Redirect(GetBaseUri(HttpContext) + PageRoutes.DownloadErrorPage.BuildRoute());

            // Create input context
            InputContext inputContext = new InputContext(accessToken, Guid.NewGuid(), language, ClientConstants.AppVersionString);

            // Decode the file name from URL format
            var decodedFileName = HttpUtility.UrlDecode(fileName);

            var request = new GetPostFileRequest(id, decodedFileName);

            try
            {
                var response = await _mvcProcessor.Process(request, inputContext);

                if (!(response.Response is GetPostFileResponse getPostFileResponse))
                {
                    return Redirect(GetBaseUri(HttpContext) + PageRoutes.DownloadErrorPage.BuildRoute());
                }

                return File(getPostFileResponse.FileStream, System.Net.Mime.MediaTypeNames.Application.Octet,
                    getPostFileResponse.OriginalName);
            }
            catch (FileNotFoundException e)
            {
                return Redirect(GetBaseUri(HttpContext) + PageRoutes.FileNotFoundErrorPage.BuildRoute());
            }
            catch (Exception)
            {
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

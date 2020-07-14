using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Appliaction.ServerActions;
using Shared.Dtos;
using Shared.Exceptions;
using Uniwiki.Server.Host.Mvc;
using Uniwiki.Server.Host.Services;
using Uniwiki.Server.Host.Services.Abstractions;

namespace Uniwiki.Server.Host.Controllers
{
    [ApiController]
    [Route("api/Server")]
    public class ServerController : ControllerBase
    {
        private readonly IMvcProcessor _mvcProcessor;
        private readonly IMvcRequestExceptionHandlerService _mvcRequestExceptionHandlerService;
        private readonly IRequestDeserializer _requestDeserializer;
        private readonly ILogger<ServerController> _logger;
        private readonly InputContextService _inputContextService;

        public ServerController(IMvcProcessor mvcProcessor, IMvcRequestExceptionHandlerService mvcRequestExceptionHandlerService, IRequestDeserializer requestDeserializer, ILogger<ServerController> logger, InputContextService inputContextService)
        {
            _mvcProcessor = mvcProcessor;
            _mvcRequestExceptionHandlerService = mvcRequestExceptionHandlerService;
            _requestDeserializer = requestDeserializer;
            _logger = logger;
            _inputContextService = inputContextService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(DataForServer dataForServer)
        {
            try
            {
                var inputContext = _inputContextService.CreateFromHttpContext(dataForServer.AccessToken, dataForServer.Language, dataForServer.Version, HttpContext); 

                _logger.LogInformation("Received Request: {Type}, ID: {RequestId}, Has token: {HasToken}, Language: {Language}", dataForServer.Type, inputContext.RequestId, inputContext.AccessToken.HasValue, dataForServer.Language);

                var request = _requestDeserializer.Deserialize(dataForServer.Request, dataForServer.Type);

                _logger.LogInformation("Successfully deserialized the request");

                var result = await _mvcProcessor.Process(request, inputContext);

                _logger.LogInformation("Processed the request");

                return new JsonResult(result);
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
    }
}

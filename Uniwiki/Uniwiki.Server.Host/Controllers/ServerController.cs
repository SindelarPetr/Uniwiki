using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Server.Appliaction.ServerActions;
using Shared.Dtos;
using Shared.Exceptions;
using Uniwiki.Server.Host.Mvc;
using Uniwiki.Server.Host.Services.Abstractions;

namespace Uniwiki.Server.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServerController : ControllerBase
    {
        private readonly IMvcProcessor _mvcProcessor;
        private readonly IMvcRequestExceptionHandlerService _mvcRequestExceptionHandlerService;
        private readonly IRequestDeserializer _requestDeserializer;

        public ServerController(IMvcProcessor mvcProcessor, IMvcRequestExceptionHandlerService mvcRequestExceptionHandlerService, IRequestDeserializer requestDeserializer)
        {
            _mvcProcessor = mvcProcessor;
            _mvcRequestExceptionHandlerService = mvcRequestExceptionHandlerService;
            _requestDeserializer = requestDeserializer;
        }

        [HttpPost]
        public async Task<ActionResult> Post(DataForServer dataForServer)
        {
            try
            {
                var inputContext = new InputContext(dataForServer.AccessToken, Guid.NewGuid(), dataForServer.Language,
                    dataForServer.Version);
                var request = _requestDeserializer.Deserialize(dataForServer.Request, dataForServer.Type);

                var result = await _mvcProcessor.Process(request, inputContext);

                return new JsonResult(result);
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
    }
}

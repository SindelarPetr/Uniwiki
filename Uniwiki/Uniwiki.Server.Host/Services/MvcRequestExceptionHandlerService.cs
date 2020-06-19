using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Exceptions;
using Shared.RequestResponse;
using Uniwiki.Server.Host.Services.Abstractions;

namespace Uniwiki.Server.Host.Services
{
    internal class MvcRequestExceptionHandlerService : IMvcRequestExceptionHandlerService
    {
        private readonly TextService _textService;

        public MvcRequestExceptionHandlerService(TextService textService)
        {
            _textService = textService;
        }

        public ActionResult HandleRequestException(RequestException exception, ControllerBase controller)
        {
            Console.WriteLine(exception); // TODO: Improve logging
            var errorResponse = new ErrorResponseDto(exception.HumanMessage);
            var dataForClient = new DataForClient<IResponse>(errorResponse, exception.Fixes);
            return controller.BadRequest(dataForClient);
        }

        public ActionResult HandleServerException(ControllerBase controller)
        {
            var errorResponse = new ErrorResponseDto(_textService.Error_ServerError);
            var dataForClient = new DataForClient<IResponse>(errorResponse, new FixResponseDto[0]);
            return controller.BadRequest(dataForClient);
        }
    }
}

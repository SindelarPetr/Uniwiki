using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Dtos;
using Shared.Exceptions;
using Shared.RequestResponse;
using Uniwiki.Server.Host.Services.Abstractions;

namespace Uniwiki.Server.Host.Services
{
    internal class MvcRequestExceptionHandlerService : IMvcRequestExceptionHandlerService
    {
        private readonly TextService _textService;
        private readonly ILogger<MvcRequestExceptionHandlerService> _logger;

        public MvcRequestExceptionHandlerService(TextService textService, ILogger<MvcRequestExceptionHandlerService> logger)
        {
            _textService = textService;
            _logger = logger;
        }

        public ActionResult HandleRequestException(RequestException exception, ControllerBase controller)
        {
            _logger.LogWarning(exception, "A Request Exception was thrown! Containing message: {ExceptionMessage}", exception.HumanMessage);
            var errorResponse = new ErrorResponseDto(exception.HumanMessage);
            var dataForClient = new DataForClient<IResponse>(errorResponse, exception.Fixes);
            return controller.BadRequest(dataForClient);
        }

        public ActionResult HandleException(Exception exception, ControllerBase controller)
        {
            _logger.LogWarning(exception, "A generic Exception was thrown! Containing message: {ExceptionMessage}", exception.Message);
            var errorResponse = new ErrorResponseDto(_textService.Error_ServerError);
            var dataForClient = new DataForClient<IResponse>(errorResponse, new FixResponseDto[0]);
            return controller.BadRequest(dataForClient);
        }
    }
}

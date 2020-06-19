using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;

namespace Uniwiki.Server.Host.Services.Abstractions
{
    public interface IMvcRequestExceptionHandlerService
    {
        ActionResult HandleRequestException(RequestException exception, ControllerBase controller);
        ActionResult HandleServerException(ControllerBase controller);
    }
}
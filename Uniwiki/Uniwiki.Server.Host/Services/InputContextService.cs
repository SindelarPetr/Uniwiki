using Microsoft.AspNetCore.Http;
using Server.Appliaction.ServerActions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uniwiki.Client.Host;

namespace Uniwiki.Server.Host.Services
{
    public class InputContextService
    {
        public InputContext CreateFromHttpContext(Guid? accessToken, Language language, string appVersion, HttpContext httpContext)
        {
            var ip = httpContext.Connection.RemoteIpAddress;
            var requestId = httpContext.TraceIdentifier;

            var inputContext = new InputContext(accessToken, requestId, language, appVersion, ip);

            return inputContext;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Server.Appliaction.ServerActions;
using Shared.Dtos;
using Shared.Exceptions;
using Shared.RequestResponse;
using Uniwiki.Client.Host;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Host.Services;
using Uniwiki.Shared.Attributes;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Server.Host.Mvc
{
    internal class MvcProcessor : IMvcProcessor
    {
        [Pure]
        private static void LogRequest(ILogger<MvcProcessor> logger, IRequest request)
        {
            var requestProps = request.GetType().GetProperties().Where(p => !Attribute.IsDefined(p, typeof(DontLogAttribute))).Select(p => $"{p.Name}='{p.GetValue(request)}'");
            
            var requestString = $"Request '{request.GetType().Name}' with ";

            if (requestProps.Any())
            {
                requestString += requestProps.Aggregate((a, b) => $"{a}; {b}");
            }
            else
            {
                requestString += "no properties";
            }

            logger.LogInformation(requestString);
        }

        private readonly IAuthenticationService _authenticationService;
        private readonly IServerActionResolver _serverActionResolver;
        private readonly TextService _textService;
        private readonly ILogger<MvcProcessor> _logger;
        private readonly ILanguageService _languageService;

        public MvcProcessor(IAuthenticationService authenticationService, IServerActionResolver serverActionResolver, TextService textService, ILogger<MvcProcessor> logger,
            ILanguageService languageService)
        {
            _authenticationService = authenticationService;
            _serverActionResolver = serverActionResolver;
            _textService = textService;
            _logger = logger;
            _languageService = languageService;
        }

        // Transforms IRequest to RequestBase
        public async Task<DataForClient<IResponse>> Process(IRequest request, InputContext inputContext)
        {
            // Log the request
            LogRequest(_logger, request);

            // Set the language
            _languageService.SetLanguage(inputContext.Language);

            // Try to get info about the user
            var userInfo = _authenticationService.TryAuthenticate(inputContext.AccessToken);
            
            // Check if authentication needs to be fixed
            var fixes = new List<FixResponseDto>();
            if(userInfo.Item1 == null && inputContext.AccessToken != null)
                fixes.Add(new FixResponseDto(_textService.Error_IdentityValidationFailed, ErrorFix.Logout));

            // Check if the client uses the right version
            if(inputContext.AppVersion != ClientConstants.AppVersionString)
            {
                fixes.Add(new FixResponseDto(_textService.Error_RefreshBrowser, ErrorFix.WrongVersion));
                throw new RequestException(_textService.Error_OldVersionOfAppUsed, fixes.ToArray());
            }

            // Create request context
            var requestContext = new RequestContext(userInfo.Item1, userInfo.Item2, inputContext.RequestId, inputContext.Language, inputContext.IpAddress);

            // Get the server action
            IServerAction action = _serverActionResolver.Resolve(request);

            // Execute the server action
            var response = await action.ExecuteActionAsync(request, requestContext);

            // Wrap the response
            var dataForClient = new DataForClient<IResponse>(response, fixes.ToArray());

            return dataForClient;
        }
    }
}

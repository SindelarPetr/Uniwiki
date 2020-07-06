using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Server.Appliaction.ServerActions;
using Shared.Dtos;
using Shared.Exceptions;
using Shared.RequestResponse;
using Uniwiki.Client.Host;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Host.Services;
using Uniwiki.Shared.Services;

namespace Uniwiki.Server.Host.Mvc
{
    internal class MvcProcessor : IMvcProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAuthenticationService _authenticationService;
        private readonly IServerActionResolver _serverActionResolver;
        private readonly TextService _textService;


        public MvcProcessor(IServiceProvider serviceProvider, IAuthenticationService authenticationService, IServerActionResolver serverActionResolver, TextService textService)
        {
            _serviceProvider = serviceProvider;
            _authenticationService = authenticationService;
            _serverActionResolver = serverActionResolver;
            _textService = textService;
        }

        // Transforms IRequest to RequestBase
        public async Task<DataForClient<IResponse>> Process(IRequest request, InputContext inputContext)
        {
            // Set the language
            _serviceProvider.GetService<TextServiceBase>().SetLanguage(inputContext.Language);

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
            var requestContext = new RequestContext(userInfo.Item1, userInfo.Item2, inputContext.RequestId, inputContext.Language);

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

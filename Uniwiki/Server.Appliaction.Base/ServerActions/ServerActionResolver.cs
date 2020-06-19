using System;
using System.Collections.Generic;
using Server.Appliaction.Services;
using Shared.Exceptions;
using Shared.RequestResponse;

namespace Server.Appliaction.ServerActions
{
    internal class ServerActionResolver : IServerActionResolver
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TextService _textService;
        private readonly Dictionary<Type, Type> _requestsAndServerActions;

        public ServerActionResolver(IServiceProvider serviceProvider, IServerActionProvider serverActionProvider, TextService textService)
        {
            _serviceProvider = serviceProvider;
            _textService = textService;
            _requestsAndServerActions = serverActionProvider.ProvideRequestsAndServerActions();
        }

        public IServerAction Resolve(IRequest request)
        {
            var requestType = request.GetType();
            var serverActionType = _requestsAndServerActions[requestType];
            var serverActionObject = _serviceProvider.GetService(serverActionType);
            var serverAction = serverActionObject as IServerAction ?? throw new RequestException(_textService.Error_NoServerAction(request));

            return serverAction;
        }
    }
}
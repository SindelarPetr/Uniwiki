
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Server.Appliaction.Services;
using Shared.Exceptions;
using Shared.RequestResponse;
using Shared.Standardizers;
using Uniwiki.Server.Persistence;

namespace Server.Appliaction.ServerActions
{

    public abstract class ServerActionBase <TRequest, TResponse> : IServerAction
        where TRequest : RequestBase<TResponse> 
        where TResponse : ResponseBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TextService _textService;

        protected abstract AuthenticationLevel AuthenticationLevel { get; }

        protected ServerActionBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _textService = serviceProvider.GetService<TextService>();
        }

        public async virtual Task<TResponse> ExecuteActionAsync(TRequest request, RequestContext context)
            => (TResponse) await ExecuteActionAsync((IRequest) request, context);

        public async virtual Task<IResponse> ExecuteActionAsync(IRequest request, RequestContext context)
        {
            // If user has too low authentication level, throw error
            ThrowIfNotAuthenticated(context.AuthenticationLevel);

            // Change the type of request from IRequest to TRequest
            var tRequest = request as TRequest;

            // Try to get standardizer and validator
            var standardizer = _serviceProvider.GetService<IStandardizer<TRequest>>();
            var validator = _serviceProvider.GetService<IValidator<TRequest>>();

            // Changing the type failed
            if (tRequest == null)
                throw new InvalidOperationException(_textService.Error_FailedToRecogniseRequest(request, typeof(TRequest)));

            // Standardize the request
            tRequest = standardizer?.Standardize(tRequest) ?? tRequest;

            // Validate the request
            var isValid = validator?.Validate(tRequest).IsValid ?? true;

            // Throw error if validation failed
            if (!isValid)
                throw new RequestException(_textService.RequestWasNotSuccessfullyValidated);

            // Execute the server request
            var result = await ExecuteAsync(tRequest, context);

            return result;
        }

        private void ThrowIfNotAuthenticated(AuthenticationLevel userLevel)
        {
            if (!Enum.IsDefined(typeof(AuthenticationLevel), userLevel))
                throw new InvalidEnumArgumentException(nameof(userLevel), (int) userLevel, typeof(AuthenticationLevel));

            if ((int)userLevel < (int)AuthenticationLevel)
            {
                if (AuthenticationLevel == AuthenticationLevel.PrimaryToken)
                    throw new RequestException(_textService.Error_YouNeedToLogInForThis); 

                throw new RequestException(_textService.Error_YouAreNotAuthorizedForThisRequest); 
            }
        }

        protected abstract Task<TResponse> ExecuteAsync(TRequest request, RequestContext context);

    }
}

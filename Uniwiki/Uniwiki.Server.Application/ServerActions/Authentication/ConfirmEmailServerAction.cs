using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.ServerActions.Authentication
{

    internal class ConfirmEmailServerAction : ServerActionBase<ConfirmEmailRequestDto, ConfirmEmailResponseDto>
    {
        private readonly IEmailConfirmationSecretRepository _emailConfirmationSecretRepository;
        private readonly ILoginService _loginService;
        private readonly TextService _textService;

        public ConfirmEmailServerAction(IServiceProvider serviceProvider, IEmailConfirmationSecretRepository emailConfirmationSecretRepository, ILoginService loginService, TextService textService) : base(serviceProvider)
        {
            _emailConfirmationSecretRepository = emailConfirmationSecretRepository;
            _loginService = loginService;
            _textService = textService;
        }

        protected override Task<ConfirmEmailResponseDto> ExecuteAsync(ConfirmEmailRequestDto request, RequestContext context)
        {
            // Get secret
            var secret = _emailConfirmationSecretRepository.FindSecret(request.Secret);

            // If email was already confirmed, then return ok response, but dont issue login token
            if (secret.Profile.IsConfirmed)
                return Task.FromResult(new ConfirmEmailResponseDto(secret.Profile.ToDto(), null));

            // If the secret was invalidated, then return error
            if (!secret.IsValid)
                throw new RequestException(_textService.Error_EmailConfirmationFailed);

            // Confirm the email
            _emailConfirmationSecretRepository.ConfirmEmail(secret);

             // Invalidate all secrets
            _emailConfirmationSecretRepository.InvalidateSecrets(secret.Profile);

            // Issue login token
            var token = _loginService.LoginUser(secret.Profile);

            // Create response
            var result = new ConfirmEmailResponseDto(secret.Profile.ToDto(), token.ToDto());

            return Task.FromResult(result);
        }

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;
    }
}

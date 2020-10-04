using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.ServerActions.Authentication
{

    internal class ConfirmEmailServerAction : ServerActionBase<ConfirmEmailRequestDto, ConfirmEmailResponseDto>
    {
        private readonly EmailConfirmationSecretRepository _emailConfirmationSecretRepository;
        private readonly ILoginService _loginService;
        private readonly TextService _textService;
        private readonly UniwikiContext _uniwikiContext;

        public ConfirmEmailServerAction(IServiceProvider serviceProvider, EmailConfirmationSecretRepository emailConfirmationSecretRepository, ILoginService loginService, TextService textService, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _emailConfirmationSecretRepository = emailConfirmationSecretRepository;
            _loginService = loginService;
            _textService = textService;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<ConfirmEmailResponseDto> ExecuteAsync(ConfirmEmailRequestDto request, RequestContext context)
        {
            // Get secret
            var secret = _uniwikiContext
                .EmailConfirmationSecrets
                .Include(s => s.Profile)
                .First(s => s.Secret == request.Secret);

            // If email was already confirmed, then return ok response, but dont issue login token
            if (secret.Profile.IsConfirmed)
            {
                var user = _uniwikiContext.Profiles.Where(p => p.Id == secret.ProfileId).ToAuthorizedUserDto();
                return Task.FromResult(new ConfirmEmailResponseDto(user, null));
            }

            // If the secret was invalidated, then return error
            if (!secret.IsValid)
            {
                throw new RequestException(_textService.Error_EmailConfirmationFailed);
            }

            // Confirm the email
            _emailConfirmationSecretRepository.ConfirmEmail(secret);

             // Invalidate all secrets
            _emailConfirmationSecretRepository.InvalidateSecrets(secret.Profile.Id);

            // Issue login token
            var token = _loginService.LoginUser(secret.ProfileId);

            // Get the authorized user
            var authorizedUser = _uniwikiContext.Profiles.Where(p => p.Id == secret.ProfileId).ToAuthorizedUserDto();

            // Create DTO from the token
            var tokenDto = token.ToLoginTokenDto().Single();

            // Create the response
            var result = new ConfirmEmailResponseDto(authorizedUser, tokenDto);

            return Task.FromResult(result);
        }

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;
    }
}

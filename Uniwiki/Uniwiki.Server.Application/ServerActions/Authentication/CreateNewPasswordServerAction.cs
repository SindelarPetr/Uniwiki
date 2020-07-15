using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.ServerActions.Authentication
{

    internal class CreateNewPasswordServerAction : ServerActionBase<CreateNewPasswordRequestDto, CreateNewPasswordResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly INewPasswordSecretRepository _newPasswordSecretRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly ITimeService _timeService;
        private readonly TextService _textService;
        private readonly IHashService _hashService;

        public CreateNewPasswordServerAction(IServiceProvider serviceProvider, INewPasswordSecretRepository newPasswordSecretRepository, IProfileRepository profileRepository, ITimeService timeService, TextService textService, IHashService hashService) : base(serviceProvider)
        {
            _newPasswordSecretRepository = newPasswordSecretRepository;
            _profileRepository = profileRepository;
            _timeService = timeService;
            _textService = textService;
            _hashService = hashService;
        }

        protected override Task<CreateNewPasswordResponseDto> ExecuteAsync(CreateNewPasswordRequestDto request, RequestContext context)
        {
            // Get the secret
            var secret = _newPasswordSecretRepository.GetValidSecretById(request.Secret);

            // Check the expiration of the secret
            if (secret.ExpirationTime < _timeService.Now)
                throw new RequestException(_textService.Error_CouldNotRefreshPassword);

            // Get profile for the provided secret
            var profile = _newPasswordSecretRepository.GetProfileForNewPasswordSecret(secret.Secret);

            // Hash the password
            var password = _hashService.HashPassword(request.NewPassword);

            // Change password
            _profileRepository.ChangePassword(profile, password.hashedPassword, password.salt);

            // Invalidate the secret
            _newPasswordSecretRepository.InvalidateSecrets(profile);

            // Create response
            var result = new CreateNewPasswordResponseDto();

            return Task.FromResult(result);
        }
    }
}

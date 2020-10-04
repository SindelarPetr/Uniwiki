using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.ServerActions.Authentication
{

    internal class CreateNewPasswordServerAction : ServerActionBase<CreateNewPasswordRequestDto, CreateNewPasswordResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly NewPasswordSecretRepository _newPasswordSecretRepository;
        private readonly ProfileRepository _profileRepository;
        private readonly ITimeService _timeService;
        private readonly TextService _textService;
        private readonly IHashService _hashService;
        private readonly UniwikiContext _uniwikiContext;

        public CreateNewPasswordServerAction(IServiceProvider serviceProvider, NewPasswordSecretRepository newPasswordSecretRepository, ProfileRepository profileRepository, ITimeService timeService, TextService textService, IHashService hashService, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _newPasswordSecretRepository = newPasswordSecretRepository;
            _profileRepository = profileRepository;
            _timeService = timeService;
            _textService = textService;
            _hashService = hashService;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<CreateNewPasswordResponseDto> ExecuteAsync(CreateNewPasswordRequestDto request, RequestContext context)
        {
            // Get the secret
            var secret = _newPasswordSecretRepository.GetValidSecret(request.Secret);

            // Check the expiration of the secret
            if (secret.ExpirationTime < _timeService.Now)
            {
                throw new RequestException(_textService.Error_CouldNotRefreshPassword);
            }

            // Get profile for the provided secret
            var profile = _uniwikiContext.Profiles.Find(secret.ProfileId);

            // Hash the password
            var password = _hashService.HashPassword(request.NewPassword);

            // Change password
            _profileRepository.ChangePassword(profile, password.hashedPassword, password.salt);

            // Invalidate the secret
            _newPasswordSecretRepository.InvalidateSecrets(profile.Id);

            // Create response
            var result = new CreateNewPasswordResponseDto();

            return Task.FromResult(result);
        }
    }
}

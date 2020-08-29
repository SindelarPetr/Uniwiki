using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.ServerActions.Authentication
{
    internal class RestorePasswordServerAction : ServerActionBase<RestorePasswordRequestDto, RestorePasswordResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly IEmailService _emailService;
        private readonly ProfileRepository _profileRepository;
        private readonly NewPasswordSecretRepository _newPasswordSecretRepository;
        private readonly ITimeService _timeService;
        private readonly TextService _textService;

        public RestorePasswordServerAction(IServiceProvider serviceProvider, IEmailService emailService, ProfileRepository profileRepository, NewPasswordSecretRepository newPasswordSecretRepository, ITimeService timeService, TextService textService)
            : base(serviceProvider)
        {
            _emailService = emailService;
            _profileRepository = profileRepository;
            _newPasswordSecretRepository = newPasswordSecretRepository;
            _timeService = timeService;
            _textService = textService;
        }

        protected override async Task<RestorePasswordResponseDto> ExecuteAsync(RestorePasswordRequestDto request, RequestContext context)
        {
            // Get user for the given email
            var profile = _profileRepository.GetProfileByEmail(request.Email);

            // Try to find some current secrets
            var currentSecret = _newPasswordSecretRepository.TryGetSecretForProfile(profile);
            if (currentSecret != null)
            {
                // If the secret was recently issued
                var nextRequestTime = currentSecret.CreationTime.Add(Constants.RestorePasswordSecretPause);
                if (nextRequestTime > _timeService.Now)
                    throw new RequestException(_textService.Error_EmailHasBeenAlreadySent);

                _newPasswordSecretRepository.InvalidateSecrets(profile);
            }

            // Create the secret
            var secret = _newPasswordSecretRepository.AddNewPasswordSecret(profile.Id, Guid.NewGuid(), _timeService.Now, _timeService.Now.Add(Constants.RestorePasswordSecretExpiration));

            // Send the email
            await _emailService.SendRestorePasswordEmail(request.Email, secret.Id);

            // Create response
            var response = new RestorePasswordResponseDto(request.Email);

            return response;
        }
    }
}
using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.ServerActions.Authentication
{
    internal class RegisterServerAction : ServerActionBase<RegisterRequestDto, RegisterResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly IEmailService _emailService;
        private readonly IProfileRepository _profileRepository;
        private readonly IEmailConfirmationSecretRepository _emailConfirmationSecretRepository;
        private readonly ITimeService _timeService;
        private readonly TextService _textService;
        private readonly IStringStandardizationService _stringStandardizationService;
        private readonly IHashService _hashService;

        public RegisterServerAction(IServiceProvider serviceProvider, IEmailService emailService, IProfileRepository profileRepository, IEmailConfirmationSecretRepository emailConfirmationSecretRepository, ITimeService timeService, TextService textService, IStringStandardizationService stringStandardizationService, IHashService hashService)
            : base(serviceProvider)
        {
            _emailService = emailService;
            _profileRepository = profileRepository;
            _emailConfirmationSecretRepository = emailConfirmationSecretRepository;
            _timeService = timeService;
            _textService = textService;
            _stringStandardizationService = stringStandardizationService;
            _hashService = hashService;
        }

        protected override async Task<RegisterResponseDto> ExecuteAsync(RegisterRequestDto request, RequestContext context)
        {
            // Try to get profile
            var profile = _profileRepository.TryGetProfileByEmail(request.Email);

            // Email is already registered
            if (profile != null)
            {
                // Email is even confirmed
                if (profile.IsConfirmed)
                {
                    throw new RequestException(_textService.Error_EmailIsAlreadyUsed(request.Email));
                }

                // Try to get an exiting secret
                var secret = _emailConfirmationSecretRepository.TryGetValidEmailConfirmationSecret(profile);

                // If the secret was already generated
                if (secret != null)
                {
                    // Check if its possible to resend it to the user
                    if (_timeService.Now < secret.CreationTime.Add(Constants.ResendRegistrationEmailMinTime))
                    {
                        throw new RequestException(_textService.Error_EmailHasBeenAlreadySent);
                    }

                    // Invalidate it
                    _emailConfirmationSecretRepository.InvalidateSecrets(profile);
                }
            }
            else
            {
                // Create url for the new profile
                string url = _stringStandardizationService.CreateUrl(request.Name + request.Surname, 
                    u => _profileRepository.TryGetProfileByUrl(u) == null);

                // Get the hash from the password
                var password = _hashService.HashPassword(request.Password);

                // Create a new profile
                profile = _profileRepository.Register(request.Email, request.Name, request.Surname, url, password.hashedPassword, password.salt, _timeService.Now);
            }

            // Generate verify email secret
            var emailConfirmationSecret = _emailConfirmationSecretRepository.GenerateEmailConfirmationSecret(profile, _timeService.Now);

            // Send the message to email
            await _emailService.SendRegisterEmail(request.Email, emailConfirmationSecret.Secret);

            return new RegisterResponseDto(profile.ToDto());
        }
    }
}

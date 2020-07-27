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
        private readonly IEmailConfirmationSenderService _emailConfirmationSenderService;

        public RegisterServerAction(IServiceProvider serviceProvider, IEmailService emailService, IProfileRepository profileRepository, IEmailConfirmationSecretRepository emailConfirmationSecretRepository, ITimeService timeService, TextService textService, IStringStandardizationService stringStandardizationService, IHashService hashService, IEmailConfirmationSenderService emailConfirmationSenderService)
            : base(serviceProvider)
        {
            _emailService = emailService;
            _profileRepository = profileRepository;
            _emailConfirmationSecretRepository = emailConfirmationSecretRepository;
            _timeService = timeService;
            _textService = textService;
            _stringStandardizationService = stringStandardizationService;
            _hashService = hashService;
            _emailConfirmationSenderService = emailConfirmationSenderService;
        }

        protected override async Task<RegisterResponseDto> ExecuteAsync(RegisterRequestDto request, RequestContext context)
        {
            // Try to get profile
            var profile = _profileRepository.TryGetProfileByEmail(request.Email);

            // Email is already registered and confirmed
            if (profile != null && profile.IsConfirmed)
            {
                throw new RequestException(_textService.Error_EmailIsAlreadyUsed(request.Email));
            }
            
            // Register user if he is not registered yet
            if(profile == null)
            {
                // Create url for the new profile
                string url = _stringStandardizationService.CreateUrl(request.Name + request.Surname,
                    u => _profileRepository.TryGetProfileByUrl(u) == null);

                // Get the hash from the password
                var password = _hashService.HashPassword(request.Password);

                // Create a new profile
                profile = _profileRepository.Register(request.Email, request.Name, request.Surname, url, password.hashedPassword, password.salt, _timeService.Now);
            }

            // Send the confirmation email
            await _emailConfirmationSenderService.SendConfirmationEmail(profile);

            return new RegisterResponseDto(profile.ToDto());
        }
    }
}

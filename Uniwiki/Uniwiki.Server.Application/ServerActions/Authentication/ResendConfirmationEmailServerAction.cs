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
    internal class ResendConfirmationEmailServerAction : ServerActionBase<ResendConfirmationEmailRequestDto, ResendConfirmationEmailResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly IEmailService _emailService;
        private readonly IEmailConfirmationSecretRepository _emailConfirmationSecretRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IInputValidationService _inputValidationService;
        private readonly ITimeService _timeService;
        private readonly TextService _textService;
        private readonly IEmailConfirmationSenderService _emailConfirmationSenderService;

        public ResendConfirmationEmailServerAction(IServiceProvider serviceProvider, IEmailService emailService, IEmailConfirmationSecretRepository emailConfirmationSecretRepository, IProfileRepository profileRepository, IInputValidationService inputValidationService, ITimeService timeService, TextService textService, IEmailConfirmationSenderService emailConfirmationSenderService) : base(serviceProvider)
        {
            _emailService = emailService;
            _emailConfirmationSecretRepository = emailConfirmationSecretRepository;
            _profileRepository = profileRepository;
            _inputValidationService = inputValidationService;
            _timeService = timeService;
            _textService = textService;
            _emailConfirmationSenderService = emailConfirmationSenderService;
        }

        protected override async Task<ResendConfirmationEmailResponseDto> ExecuteAsync(ResendConfirmationEmailRequestDto request, RequestContext context)
        {
            // Standardize mail
            var email = request.Email.StandardizeEmail();

            // Validate input
            _inputValidationService.ValidateEmail(email);

            // Get profile
            var profile = _profileRepository.GetProfileByEmail(email);

            // Throw error if the user is already confirmed
            if (profile.IsConfirmed)
                throw new RequestException(_textService.ResendConfirmation_ProfileIsAlreadyConfirmed);

            // Send the new email confirmation secret
            await _emailConfirmationSenderService.SendConfirmationEmail(profile);

            // Create the response
            var response = new ResendConfirmationEmailResponseDto();

            return response;
        }
    }
}
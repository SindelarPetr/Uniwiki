using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.ServerActions.Authentication
{
    internal class ResendConfirmationEmailServerAction : ServerActionBase<ResendConfirmationEmailRequestDto, ResendConfirmationEmailResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly ProfileRepository _profileRepository;
        private readonly IInputValidationService _inputValidationService;
        private readonly TextService _textService;
        private readonly IEmailConfirmationSenderService _emailConfirmationSenderService;

        public ResendConfirmationEmailServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, IInputValidationService inputValidationService, TextService textService, IEmailConfirmationSenderService emailConfirmationSenderService) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _inputValidationService = inputValidationService;
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
                throw new RequestException(_textService.ResendConfirmation_ProfileIsAlreadyConfirmed(profile.Email));

            // Send the new email confirmation secret
            await _emailConfirmationSenderService.SendConfirmationEmail(profile);

            // Create the response
            var response = new ResendConfirmationEmailResponseDto();

            return response;
        }
    }
}
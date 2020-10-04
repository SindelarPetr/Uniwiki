using System;
using System.Linq;
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
        private readonly EmailConfirmationSenderService _emailConfirmationSenderService;
        private readonly UniwikiContext _uniwikiContext;

        public ResendConfirmationEmailServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, IInputValidationService inputValidationService, TextService textService, EmailConfirmationSenderService emailConfirmationSenderService, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _inputValidationService = inputValidationService;
            _textService = textService;
            _emailConfirmationSenderService = emailConfirmationSenderService;
            _uniwikiContext = uniwikiContext;
        }

        protected override async Task<ResendConfirmationEmailResponseDto> ExecuteAsync(ResendConfirmationEmailRequestDto request, RequestContext context)
        {
            // Standardize mail
            var email = request.Email.StandardizeEmail();

            // Validate input
            _inputValidationService.ValidateEmail(email);

            // Get profile
            var profile = _uniwikiContext
                .Profiles
                .Select(p => new {p.Email, p.IsConfirmed, p.Id})
                .First(p => p.Email == email);

            // Throw error if the user is already confirmed
            if (profile.IsConfirmed)
            {
                throw new RequestException(_textService.ResendConfirmation_ProfileIsAlreadyConfirmed(profile.Email));
            }

            // Send the new email confirmation secret
            await _emailConfirmationSenderService.SendConfirmationEmail(profile.Id, profile.Email);

            // Create the response
            var response = new ResendConfirmationEmailResponseDto();

            return response;
        }
    }
}
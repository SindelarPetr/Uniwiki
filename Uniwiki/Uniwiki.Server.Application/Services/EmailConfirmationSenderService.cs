using Shared.Exceptions;
using Shared.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared;

namespace Uniwiki.Server.Application.Services
{
    internal class EmailConfirmationSenderService : IEmailConfirmationSenderService
    {
        private readonly IEmailConfirmationSecretRepository _emailConfirmationSecretRepository;
        private readonly ITimeService _timeService;
        private readonly IEmailService _emailService;
        private readonly TextService _textService;

        public EmailConfirmationSenderService(IEmailConfirmationSecretRepository emailConfirmationSecretRepository, ITimeService timeService, IEmailService emailService, TextService textService)
        {
            _emailConfirmationSecretRepository = emailConfirmationSecretRepository;
            _timeService = timeService;
            _emailService = emailService;
            _textService = textService;
        }

        /// <summary>
        /// Generates, sends and saves the confirmation email for the specified user.
        /// </summary>
        public async Task SendConfirmationEmail(ProfileModel profile)
        {
            // Try to get an existing secret
            var currentSecret = _emailConfirmationSecretRepository.TryGetValidEmailConfirmationSecret(profile);

            // if current secret exists and its not expired
            if (currentSecret != null && currentSecret.CreationTime.Add(Constants.ResendRegistrationEmailMinTime) > _timeService.Now)
                throw new RequestException(_textService.Error_EmailHasBeenAlreadySent);

            // Invalidate all old secret(s)
            _emailConfirmationSecretRepository.InvalidateSecrets(profile);

            // Generate email confirmation secret
            var emailConfirmationSecret = _emailConfirmationSecretRepository.GenerateEmailConfirmationSecret(profile, _timeService.Now);

            // Send the message to email
            await _emailService.SendRegisterEmail(profile.Email, emailConfirmationSecret.Secret);

            // Save the email secret to the DB. We have to do it after the email was sent - if it will fail it throws exception and the secret will not be saved.
            _emailConfirmationSecretRepository.SaveEmailConfirmationSecret(emailConfirmationSecret);
        }
    }
}

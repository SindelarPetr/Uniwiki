using Shared.Exceptions;
using Shared.Services.Abstractions;
using System;
using System.Threading.Tasks;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared;

namespace Uniwiki.Server.Application.Services
{
    internal class EmailConfirmationSenderService
    {
        private readonly EmailConfirmationSecretRepository _emailConfirmationSecretRepository;
        private readonly ITimeService _timeService;
        private readonly IEmailService _emailService;
        private readonly TextService _textService;

        public EmailConfirmationSenderService(EmailConfirmationSecretRepository emailConfirmationSecretRepository, ITimeService timeService, IEmailService emailService, TextService textService)
        {
            _emailConfirmationSecretRepository = emailConfirmationSecretRepository;
            _timeService = timeService;
            _emailService = emailService;
            _textService = textService;
        }

        /// <summary>
        /// Generates, sends and saves the confirmation email for the specified user.
        /// </summary>
        public async Task SendConfirmationEmail(Guid profileId, string profileEmail)
        {
            // Try to get an existing secret
            var currentSecret = _emailConfirmationSecretRepository.TryGetValidEmailConfirmationSecret(profileId);

            // if current secret exists and its not expired
            if (currentSecret != null && currentSecret.CreationTime.Add(Constants.ResendRegistrationEmailMinTime) > _timeService.Now)
            {
                throw new RequestException(_textService.Error_EmailHasBeenAlreadySent);
            }

            // Invalidate all old secret(s)
            _emailConfirmationSecretRepository.InvalidateSecrets(profileId);

            // Add it to the DB
            var emailConfirmationSecret = _emailConfirmationSecretRepository.AddEmailConfirmationSecret(profileId, Guid.NewGuid(), _timeService.Now);

            // Send the message to email
            await _emailService.SendRegisterEmail(profileEmail, emailConfirmationSecret.Secret);

            // Save the email secret to the DB. We have to do it after the email was sent - if it will fail it throws exception and the secret will not be saved.
            _emailConfirmationSecretRepository.SaveEmailConfirmationSecret(emailConfirmationSecret);
        }
    }
}

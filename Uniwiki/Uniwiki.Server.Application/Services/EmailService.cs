using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shared.Exceptions;
using Uniwiki.Server.Application.Configuration;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Shared;

namespace Uniwiki.Server.Application.Services
{
    internal class EmailService : IEmailService
    {
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly TextService _textService;
        private readonly UniwikiConfiguration _uniwikiConfiguration;
        private readonly ILogger<EmailService> _logger;
        private readonly string _baseUrl;
        public bool SendingEmailsDisabled { get; private set; }

        public EmailService(IHttpContextAccessor httpContextAccessor, IEmailTemplateService emailTemplateService, TextService textService, UniwikiConfiguration uniwikiConfiguration, ILogger<EmailService> logger)
        {
            _emailTemplateService = emailTemplateService;
            _textService = textService;
            _uniwikiConfiguration = uniwikiConfiguration;
            _logger = logger;
            _baseUrl = GetBaseUri(httpContextAccessor.HttpContext);
        }

        public void DisableSendingEmails() => SendingEmailsDisabled = true;

        private string GetBaseUri(HttpContext context)
        {
            // If there is not context, then dont care about it
            if (context == null)
            {
                return string.Empty;
            }

            var request = context.Request;

            var host = request.Host.ToUriComponent();

            var pathBase = request.PathBase.ToUriComponent();

            return $"{request.Scheme}://{host}{pathBase}";
        }

        private async Task SendEmail(string recipientEmail, string subject, string messageText, bool isHtml = true)
        {
            // Dont send emails if the address of Uniwiki is unknown
            if(string.IsNullOrWhiteSpace(_baseUrl) || SendingEmailsDisabled)
                return;

            // Get the configuration
            var senderAddress = _uniwikiConfiguration.Email.SenderAddress;
            var password = _uniwikiConfiguration.Email.Password;
            var host = _uniwikiConfiguration.Email.Host;
            var port = _uniwikiConfiguration.Email.Port;
            var displayName = _uniwikiConfiguration.Email.DisplayName;

            // Prepare message
            MailMessage message = new MailMessage();
            message.From = new MailAddress(senderAddress, displayName);
            message.To.Add(new MailAddress(recipientEmail));
            message.Subject = subject;
            message.IsBodyHtml = isHtml; // Mark message body as html  
            message.Body = messageText;

            // Prepare SMTP client
            SmtpClient smtp = new SmtpClient();
            smtp.Port = port;
            smtp.Host = host;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(senderAddress, password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                await smtp.SendMailAsync(message);
            }
            catch (Exception e)
            {
                _logger.LogError(0, e, $"Encountered a problem while sending an email to: '{recipientEmail}'");
                throw new RequestException(_textService.Error_CouldNotSendEmail(recipientEmail));
            }
        }

        public Task SendRestorePasswordEmail(string recipientEmail, Guid secret)
        {
            var subject = _textService.Email_RestorePasswordSubject;
            var link =
                $"{_baseUrl}/{ PageRoutes.CreateNewPasswordPage.BuildRoute(secret.ToString()) }";
            var message = _emailTemplateService.GetRestorePasswordText(link);
            
            return SendEmail(recipientEmail, subject, message, true);
        }

        public Task SendRegisterEmail(string recipientEmail, Guid secret)
        {
            var subject = _textService.Email_RegisterSubject;
            var link =
                $"{_baseUrl}/{ PageRoutes.EmailConfirmedPage.BuildRoute(secret.ToString(), recipientEmail) }";
            var message = _emailTemplateService.GetVerifyEmailText(link);

            return SendEmail(recipientEmail, subject, message, true);
        }
    }
}

using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shared.Exceptions;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Shared;

namespace Uniwiki.Server.Application.Services
{
    internal class EmailService : IEmailService
    {
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly TextService _textService;
        private readonly string _baseUrl;
        public EmailService(IHttpContextAccessor httpContextAccessor, IEmailTemplateService emailTemplateService, TextService textService)
        {
            _emailTemplateService = emailTemplateService;
            _textService = textService;
            _baseUrl = GetBaseUri(httpContextAccessor.HttpContext);
        }

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

        private async Task SendEmail(string recipientEmail, string subject, string messageText, bool isHtml = false)
        {
            // Dont send emails if the address of Uniwiki is unknown
            if(string.IsNullOrWhiteSpace(_baseUrl))
                return;

            var senderAddress = "uniwiki.official@gmail.com";
            var password = "upnaiswsiwko12";
            var host = "smtp.gmail.com";
            var port = 587;

            // Prepare message
            MailMessage message = new MailMessage();
            message.From = new MailAddress(senderAddress, "Uniwiki");
            message.To.Add(new MailAddress(recipientEmail));
            message.Subject = subject;
            message.IsBodyHtml = isHtml; //to make message body as html  
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
                Console.WriteLine(e);
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

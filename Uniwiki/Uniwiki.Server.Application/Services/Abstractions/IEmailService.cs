using System;
using System.Threading.Tasks;

namespace Uniwiki.Server.Application.Services.Abstractions
{
    public interface IEmailService
    {
        Task SendRestorePasswordEmail(string recipientEmail, Guid secret);
        Task SendRegisterEmail(string recipientEmail, Guid secret);

        /// <summary>
        /// Disables the sending emails. This is for initialization purposes, when we are registering users like a@a.cz, we dont want to send emails there.
        /// </summary>
        void DisableSendingEmails();
    }
}
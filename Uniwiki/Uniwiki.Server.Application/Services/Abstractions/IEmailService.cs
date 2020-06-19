using System;
using System.Threading.Tasks;

namespace Uniwiki.Server.Application.Services.Abstractions
{
    public interface IEmailService
    {
        Task SendRestorePasswordEmail(string recipientEmail, Guid secret);
        Task SendRegisterEmail(string recipientEmail, Guid secret);
    }
}
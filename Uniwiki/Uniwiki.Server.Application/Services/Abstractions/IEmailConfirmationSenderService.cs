using System.Threading.Tasks;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Application.Services.Abstractions
{
    public interface IEmailConfirmationSenderService
    {
        Task SendConfirmationEmail(ProfileModel profile);
    }
}
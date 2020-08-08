using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Application.Services.Abstractions
{
    internal interface ILoginService
    {
        LoginTokenModel LoginUser(string email, string password);
        LoginTokenModel LoginUser(ProfileModel profileModel);
    }
}
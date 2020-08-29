using System;
using System.Threading.Tasks;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Client.Host.Services.Abstractions
{
    public interface ILoginService
    {
        bool IsAuthenticated { get; }
        AuthorizedUserDto? User { get; }
        LoginTokenDto? LoginToken { get; }
        Task Login(LoginRequestDto loginRequest, Action finalAction);
        Task Logout();
    }
}
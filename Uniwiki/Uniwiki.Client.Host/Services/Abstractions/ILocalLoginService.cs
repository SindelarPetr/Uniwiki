using System.Threading.Tasks;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Client.Host.Services.Abstractions
{
    public interface ILocalLoginService
    {
        bool IsAuthenticated { get; }
        ProfileDto? User { get; }
        LoginTokenDto? LoginToken { get; }

        Task LocalLogout();
        Task<ProfileDto> LocalLogin(ProfileDto user, LoginTokenDto loginToken);
        Task InitializeLogin();
    }
}
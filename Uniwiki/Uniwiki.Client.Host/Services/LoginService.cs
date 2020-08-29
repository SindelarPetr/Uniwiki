using System;
using System.Threading.Tasks;
using Uniwiki.Client.Host.Services.Abstractions;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Client.Host.Services
{
    internal class LoginService : ILoginService
    {
        private readonly IRequestSender _requestSender;
        private readonly LocalLoginService _localLoginService;

        public bool IsAuthenticated => _localLoginService.IsAuthenticated;
        public AuthorizedUserDto? User => _localLoginService.User;
        public LoginTokenDto? LoginToken => _localLoginService.LoginToken;

        public LoginService(IRequestSender requestSender, LocalLoginService localLoginService)
        {
            _requestSender = requestSender;
            _localLoginService = localLoginService;
        }

        public async Task Login(LoginRequestDto loginRequest, Action finalAction)
        {
            // Send the login request
            var response = await _requestSender.SendRequestAsync(loginRequest, finalAction);

            await _localLoginService.LocalLogin(response.User, response.LoginToken);
        }

        public Task Logout()
        {
            return _localLoginService.LocalLogout();
        }
    }
}

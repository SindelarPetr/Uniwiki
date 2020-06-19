using System.Threading.Tasks;
using Uniwiki.Client.Host.Services.Abstractions;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse.Authentication;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Client.Host.Services
{
    internal class LocalLoginService : ILocalLoginService
    {
        private readonly ILocalAuthenticationStateProvider _localAuthenticationStateProvider;
        private readonly ILocalStorageManagerService _localStorageManagerService;
        private readonly ITimeService _timeService;

        public bool IsAuthenticated => User != null && LoginToken != null;
        public ProfileDto? User { get; private set; }
        public LoginTokenDto? LoginToken { get; private set; }

        public LocalLoginService(ILocalAuthenticationStateProvider localAuthenticationStateProvider, ILocalStorageManagerService localStorageManagerService, ITimeService timeService)
        {
            _localAuthenticationStateProvider = localAuthenticationStateProvider;
            _localStorageManagerService = localStorageManagerService;
            _timeService = timeService;
        }

        public async Task InitializeLogin()
        {
            var loginToken = await _localStorageManagerService.GetLoginToken();
            var loginProfile = await _localStorageManagerService.GetLoginProfile();

            if (loginToken == null || loginProfile == null || loginToken.Expiration.AddMinutes(10) < _timeService.Now)
                return;

            User = loginProfile;
            LoginToken = loginToken;

            // Notify the rest of the app about authentication
            _localAuthenticationStateProvider.SetAsLoggedIn(LoginToken.PrimaryTokenId);
        }

        public async Task<ProfileDto> LocalLogin(ProfileDto user, LoginTokenDto loginToken)
        {
            User = user;
            LoginToken = loginToken;

            // Remove all current credentials - this fixes problem when the credentials in the local storage are broken
            await _localStorageManagerService.RemoveLoginToken();
            await _localStorageManagerService.RemoveLoginProfile();

            // Save credentials to local storage
            await _localStorageManagerService.SetLoginProfile(user);
            await _localStorageManagerService.SetLoginToken(loginToken);

            // Notify the rest of the app about authentication
            _localAuthenticationStateProvider.SetAsLoggedIn(loginToken.PrimaryTokenId);

            return User;
        }

        public async Task LocalLogout()
        {
            User = null;
            LoginToken = null;

            await _localStorageManagerService.RemoveLoginProfile();
            await _localStorageManagerService.RemoveLoginToken();

            // Notify the rest of the app about authentication
            _localAuthenticationStateProvider.SetAsLoggedOut();
        }

    }
}
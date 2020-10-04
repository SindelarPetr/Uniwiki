using Shared.Services.Abstractions;
using System.Threading.Tasks;
using Uniwiki.Client.Host.Services.Abstractions;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Client.Host.Services
{
    public class LocalLoginService
    {
        private readonly ILocalAuthenticationStateProvider _localAuthenticationStateProvider;
        private readonly ILocalStorageManagerService _localStorageManagerService;
        private readonly ITimeService _timeService;
        private readonly StaticStateService _staticStateService;

        public bool IsAuthenticated => User != null && LoginToken != null;
        public AuthorizedUserDto? User { get; private set; }
        public LoginTokenDto? LoginToken { get; private set; }

        public LocalLoginService(ILocalAuthenticationStateProvider localAuthenticationStateProvider, ILocalStorageManagerService localStorageManagerService, ITimeService timeService, StaticStateService staticStateService)
        {
            _localAuthenticationStateProvider = localAuthenticationStateProvider;
            _localStorageManagerService = localStorageManagerService;
            _timeService = timeService;
            _staticStateService = staticStateService;
        }

        // TODO: Sould I actually use this?
        public async Task InitializeLogin()
        {
            var loginToken = await _localStorageManagerService.GetLoginToken();
            var loginProfile = await _localStorageManagerService.GetLoginProfile();

            if (loginProfile?.HasHomeStudyGroup ?? false)
            {
                _staticStateService.SetSelectedStudyGroup(
                    new StudyGroupToSelectDto(
                        loginProfile.HomeStudyGroup!.LongName, 
                        loginProfile.HomeStudyGroup.ShortName, 
                        loginProfile.HomeStudyGroup.StudyGroupId, 
                        loginProfile.HomeStudyGroup.UniversityShortName,
                        loginProfile.HomeStudyGroup.UniversityId)
                );
            }

            if (loginToken == null || loginProfile == null || loginToken.Expiration.AddMinutes(10) < _timeService.Now)
            {
                return;
            }

            User = loginProfile;
            LoginToken = loginToken;

            // Notify the rest of the app about authentication
            _localAuthenticationStateProvider.SetAsLoggedIn(LoginToken.PrimaryTokenId);
        }

        public async Task<AuthorizedUserDto> LocalLogin(AuthorizedUserDto user, LoginTokenDto loginToken)
        {
            User = user;
            LoginToken = loginToken;

            // Remove all current credentials - this fixes problem when the credentials in the local storage are broken
            await _localStorageManagerService.RemoveLoginToken();
            await _localStorageManagerService.RemoveLoginProfile();

            // Save credentials to local storage
            await _localStorageManagerService.SetLoginProfile(user);
            await _localStorageManagerService.SetLoginToken(loginToken);

            // Set static state
            if(user.HasHomeStudyGroup)
            {
                _staticStateService.SetSelectedStudyGroup(
                    new StudyGroupToSelectDto(
                        user.HomeStudyGroup!.LongName, 
                        user.HomeStudyGroup.ShortName, 
                        user.HomeStudyGroup.StudyGroupId,
                        user.HomeStudyGroup.UniversityShortName,
                        user.HomeStudyGroup.UniversityId));
            }

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

        public Task UpdateUser(AuthorizedUserDto user)
        {
            // Dont do anything if the user is not authenticated
            if (!IsAuthenticated)
            {
                return Task.CompletedTask;
            }

            User = user;

            return _localStorageManagerService.SetLoginProfile(user);
        }
    }
}
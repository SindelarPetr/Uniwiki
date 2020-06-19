using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Uniwiki.Client.Host.Services;
using Uniwiki.Client.Host.Services.Abstractions;
using Uniwiki.Shared;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Client.Host.Pages
{
    public partial class ProfilePage
    {
        [Inject] private IRequestSender RequestSender { get; set; }
        [Inject] private ILoginService LoginService { get; set; }
        [Inject] private INavigationService NavigationService { get; set; }
        [Parameter] public string Url { get; set; }

        private bool _signedUser;
        private bool _isLoading;
        private string _name;
        private string _surname;
        private string _profileImageSrc;
        protected override async Task OnParametersSetAsync()
        {
            _isLoading = true;
            await base.OnParametersSetAsync();

            _signedUser = LoginService.IsAuthenticated && Url == LoginService.User.NameIdentifier;

            if (_signedUser)
            {
                _name = LoginService.User.FirstName;
                _surname = LoginService.User.FamilyName;
                _profileImageSrc = LoginService.User.ProfilePictureSrc;
            }
            else
            {
                var response = await RequestSender.SendRequestAsync(new GetProfileRequest(Url), () =>
                    {
                        _isLoading = false;
                        StateHasChanged();
                    });
                _name = response.Profile.FirstName;
                _surname = response.Profile.FamilyName;
                _profileImageSrc = response.Profile.ProfilePictureSrc;
            }

            _isLoading = false;
        }


        public async Task Logout()
        {
            await LoginService.Logout();

            NavigationService.NavigateTo(PageRoutes.LoginPage.BuildRoute());
        }

    }
}

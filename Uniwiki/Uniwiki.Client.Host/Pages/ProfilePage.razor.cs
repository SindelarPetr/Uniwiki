using System;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Uniwiki.Client.Host.Extensions;
using Uniwiki.Client.Host.Modals;
using Uniwiki.Client.Host.Services;
using Uniwiki.Client.Host.Services.Abstractions;
using Uniwiki.Shared;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Client.Host.Pages
{
    public partial class ProfilePage
    {
        [Inject] IRequestSender RequestSender { get; set; } = null!;
        [Inject] ILoginService LoginService { get; set; } = null!;
        [Inject] INavigationService NavigationService { get; set; } = null!;
        [Inject] LocalLoginService LocalLoginService { get; set; } = null!;
        [Inject] IModalService ModalService { get; set; } = null!;
        [Inject] StaticStateService StaticStateService { get; set; } = null!;

        [Parameter] public string Url { get; set; } = null!;

        private GetProfileResponse _pageData = null!;

        private bool _edittingHomeFaculty;

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            _pageData = await RequestSender.SendRequestAsync(new GetProfileRequest(Url));

            // Update the authenticated profile
            if (_pageData.IsAuthenticated)
            {
                await LocalLoginService.UpdateUser(_pageData.AuthorizedUser!);
            }
        }

        public async Task Logout()
        {
            await LoginService.Logout();

            NavigationService.NavigateTo(PageRoutes.LoginPage.BuildRoute());
        }

        public async Task HandleSelectMyUniversityAndFaculty()
        {
            // Show the dialog with the faculties
            var studyGroup = await ModalService.SelectStudyGroup(TextService);

            if(studyGroup != null)
            {
                await EditHomeFaculty(studyGroup.StudyGroupId);
            }
        }

        public async Task HandleRemoveMyUniversityAndFaculty()
        {
            // Show confirmation dialog
            var confirmed = await ModalService.Confirm(TextService.ProfilePage_ConfirmRemoveHomeUniversityAndFaculty);

            if (!confirmed)
            {
                return;
            }

            await EditHomeFaculty(null);
        }

        private async Task EditHomeFaculty(Guid? homeFacultyId)
        {
            _edittingHomeFaculty = true;

            StateHasChanged();

            // Create the request
            var request = new EditHomeFacultyRequestDto(homeFacultyId);

            // Send the request
            var response = await RequestSender.SendRequestAsync(request, () => { _edittingHomeFaculty = false; StateHasChanged(); });

            // Update the profile
            await LocalLoginService.UpdateUser(response.AuthorizedUser!);

            // Display the updated user
            _pageData.Profile = response.Profile;

            // Save the new home university to the static state
            StaticStateService.SetSelectedStudyGroup(
                response.Profile.HomeStudyGroup == null ? null 
                    : new StudyGroupToSelectDto(
                    response.Profile.HomeStudyGroup.LongName, 
                    response.Profile.HomeStudyGroup.ShortName, 
                    response.Profile.HomeStudyGroup.StudyGroupId, 
                    response.Profile.HomeStudyGroup.UniversityShortName,
                    response.Profile.HomeStudyGroup.UniversityId));
        }
    }
}

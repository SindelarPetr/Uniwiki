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

namespace Uniwiki.Client.Host.Pages
{
    public partial class ProfilePage
    {
        [Inject] private IRequestSender RequestSender { get; set; }
        [Inject] private ILoginService LoginService { get; set; }
        [Inject] private INavigationService NavigationService { get; set; }
        [Inject] private LocalLoginService LocalLoginService { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        [Inject] private StaticStateService StaticStateService { get; set; }

        [Parameter] public string Url { get; set; }

        private GetProfileResponse _pageData;

        private bool _edittingHomeFaculty;

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            _pageData = await RequestSender.SendRequestAsync(new GetProfileRequest(Url));

            // Update the authenticated profile
            if (_pageData.Authenticated)
            {
               await LocalLoginService.UpdateUser(_pageData.AuthenticatedUser);
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
            var modal = ModalService.Show<SelectStudyGroupModal>(TextService.SelectFacultyModal_Title);

            // Wait for the result
            var result = await modal.Result;

            // if the user cancelled the dialog, dont do anything
            if (result.Cancelled)
                return;

            // Get the selected faculty
            var selectedFaculty = (StudyGroupDto)result.Data;

            await EditHomeFaculty(selectedFaculty.Id);
        }

        public async Task HandleRemoveMyUniversityAndFaculty()
        {
            // Show confirmation dialog
            var confirmed = await ModalService.Confirm(TextService.ProfilePage_ConfirmRemoveHomeUniversityAndFaculty);

            if (!confirmed)
                return;

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
            await LocalLoginService.UpdateUser(response.Profile);

            // Display the updated user
            _pageData.Profile = response.Profile;

            // Save the new home university to the static state
            StaticStateService.SetSelectedStudyGroup(new StudyGroupToSelectDto(response.Profile.HomeStudyGroupLongName, response.Profile.HomeStudyGroupShortName, response.Profile.HomeStudyGroupId.Value, response.Profile.HomeStudyGroupUniversityShortName) response.Profile.HomeFaculty);
        }
    }
}

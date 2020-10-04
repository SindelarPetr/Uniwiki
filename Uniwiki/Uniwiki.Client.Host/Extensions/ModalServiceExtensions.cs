using System.Threading.Tasks;
using Blazored.Modal.Services;
using Uniwiki.Client.Host.Modals;
using Uniwiki.Client.Host.Services;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Client.Host.Extensions
{
    public static class ModalServiceExtensions
    {
        public static async Task<bool> Confirm(this IModalService modalService, string text)
        {
            var modal = modalService.Show<ConfirmModal>(text);
            var result = await modal.Result;

            return !result.Cancelled;
        }

        internal static async Task<StudyGroupToSelectDto?> SelectStudyGroup(this IModalService modalService, TextService textService)
        {
            // Show modal window
            var modal = modalService.Show<SelectStudyGroupModal>(textService.SelectFacultyModal_Title);
            var result = await modal.Result;

            // Check if the modal is canceled
            if (result.Cancelled)
            {
                return null;
            }

            // Get selected study group
            var studyGroup = result.Data as StudyGroupToSelectDto;

            return studyGroup;
        }
    }
}

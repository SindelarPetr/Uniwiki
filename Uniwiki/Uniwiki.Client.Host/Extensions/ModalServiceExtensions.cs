using System.Threading.Tasks;
using Blazored.Modal.Services;
using Uniwiki.Client.Host.Modals;

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
    }
}

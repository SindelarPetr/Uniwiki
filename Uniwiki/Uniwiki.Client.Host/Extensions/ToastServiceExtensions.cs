using Blazored.Toast.Services;
using Uniwiki.Client.Host.Services;

namespace Uniwiki.Client.Host.Extensions
{
    internal static class ToastServiceExtensions
    {
        public static void ShowLoginRequired(this IToastService toastService, TextService textService)
        {
            toastService.ShowInfo(textService.Toast_YouNeedToLogIn);
        }
    }
}

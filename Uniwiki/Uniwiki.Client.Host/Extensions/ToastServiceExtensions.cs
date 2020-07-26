using Blazored.Toast.Services;
using Uniwiki.Client.Host.Services;
using Uniwiki.Client.Host.Services.Abstractions;

namespace Uniwiki.Client.Host.Extensions
{
    internal static class ToastServiceExtensions
    {
        public static void ShowLoginRequired(this IToastService toastService, TextService textService)
        {
            toastService.ShowInfo(textService.Toast_YouNeedToLogIn, textService.Toast_Info);
        }
    }
}

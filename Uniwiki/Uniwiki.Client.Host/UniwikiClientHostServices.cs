using System.Net.Http;
using System.Runtime.CompilerServices;
using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Uniwiki.Client.Host.Authentication;
using Uniwiki.Client.Host.Services;
using Uniwiki.Client.Host.Services.Abstractions;
using Uniwiki.Shared;
using Uniwiki.Shared.Services;

[assembly: InternalsVisibleTo("Uniwiki.Tests")]
namespace Uniwiki.Client.Host
{
    public static class UniwikiClientHostServices
    {
        public static void AddUniwikiClientHostServices(this IServiceCollection services)
        {
            services.AddUniwikiSharedServices();

            services.AddSingleton<IRequestSender, RequestSender>();
            services.AddSingleton<ILoginService, LoginService>();
            services.AddSingleton<ILocalLoginService, LocalLoginService>();
            services.AddSingleton<ILocalAuthenticationStateProvider, LocalAuthenticationStateProvider>();
            services.AddSingleton<AuthenticationStateProvider>(
                s => (LocalAuthenticationStateProvider)s.GetService<ILocalAuthenticationStateProvider>());
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddTransient<IPeriodicalTimer, PeriodicalTimer>();
            services.AddSingleton<IJsInteropService, JsInteropService>();
            services.AddSingleton<ILanguageManagerService, LanguageManagerService>();
            services.AddSingleton<ILocalStorageManagerService, LocalStorageManagerService>();
            services.AddSingleton<TextService>();
            services.AddSingleton<TextServiceBase>(p => p.GetService<TextService>());
            services.AddSingleton<IFixingService, FixingService>();
            services.AddSingleton<IScrollService, ScrollService>();
            services.AddSingleton<IStaticStateService, StaticStateService>();

            services.AddSingleton(new HttpClient());
            services.AddOptions();
            services.AddAuthorizationCore();
            services.AddBlazoredLocalStorage();
            services.AddBlazoredModal();
            services.AddSingleton<IToastService, ToastService>(); // Cannot use AddBlazoredToast(), because it needs to be singleton (that one is scoped)
            services.AddSingleton<IHttpService, HttpService>();

        }
    }
}

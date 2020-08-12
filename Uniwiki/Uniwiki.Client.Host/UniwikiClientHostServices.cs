using System.Net.Http;
using System.Runtime.CompilerServices;
using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Shared.Services.Abstractions;
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

            services.AddScoped<IRequestSender, RequestSender>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILocalLoginService, LocalLoginService>();
            services.AddScoped<ILocalAuthenticationStateProvider, LocalAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(
                s => (LocalAuthenticationStateProvider)s.GetService<ILocalAuthenticationStateProvider>());
            services.AddScoped<INavigationService, NavigationService>();
            services.AddTransient<IPeriodicalTimer, PeriodicalTimer>();
            services.AddScoped<IJsInteropService, JsInteropService>();
            services.AddScoped<ILanguageManagerService, LanguageManagerService>();
            services.AddScoped<ILocalStorageManagerService, LocalStorageManagerService>();
            services.AddScoped<TextService>();
            services.AddScoped<IFixingService, FixingService>();
            services.AddScoped<IScrollService, ScrollService>();
            services.AddScoped<IStaticStateService, StaticStateService>();
            services.AddScoped<IApplicationHostEnvironment, ClientHostEnvironment>();
            services.AddTransient<IFileUploadQueueService, FileUploadQueueService>();

            services.AddSingleton(new HttpClient());
            services.AddOptions();
            services.AddAuthorizationCore();
            services.AddBlazoredLocalStorage();
            services.AddBlazoredModal();
            services.AddBlazoredToast();
            services.AddScoped<IHttpService, HttpService>();
            
        }
    }
}

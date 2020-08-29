using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services.Abstractions;
using Uniwiki.Client.Host.Services.Abstractions;

namespace Uniwiki.Client.Host.Extensions
{
    public static class WebAssemblyHostExtensions
    {
        public static async Task InitializeClient(this WebAssemblyHost host, string baseAddress)
        {
            host.Services.GetRequiredService<IApplicationHostEnvironment>();
            await host.Services.GetRequiredService<ILanguageManagerService>().InitializeLanguage();
            await host.Services.GetRequiredService<LocalLoginService>().InitializeLogin();
            await host.Services.GetRequiredService<IScrollService>().InitializeScroll();
            host.Services.GetRequiredService<HttpClient>().BaseAddress = new Uri(baseAddress);
        }

    }
}

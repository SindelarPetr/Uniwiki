using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Uniwiki.Client.Host.Services.Abstractions;

namespace Uniwiki.Client.Host.Extensions
{
    public static class WebAssemblyHostExtensions
    {
        public static async Task InitializeClient(this WebAssemblyHost host, string baseAddress)
        {
            await host.Services.GetRequiredService<ILanguageManagerService>().InitializeLanguage();
            await host.Services.GetRequiredService<ILocalLoginService>().InitializeLogin();
            await host.Services.GetRequiredService<IScrollService>().InitializeScroll();
            host.Services.GetRequiredService<HttpClient>().BaseAddress = new Uri(baseAddress);
        }

    }
}

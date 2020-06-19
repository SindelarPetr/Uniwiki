using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Uniwiki.Client.Host.Services.Abstractions;

namespace Uniwiki.Client.Host.Extensions
{
    public static class WebAssemblyHostExtensions
    {
        public static async Task InitializeClient(this WebAssemblyHost host)
        {
           await host.Services.GetService<ILanguageManagerService>().InitializeLanguage();
           await host.Services.GetService<ILocalLoginService>().InitializeLogin();
           await host.Services.GetService<IScrollService>().InitializeScroll();
        }

    }
}

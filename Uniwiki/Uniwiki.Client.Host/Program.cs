using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Uniwiki.Client.Host.Extensions;

namespace Uniwiki.Client.Host
{
    public class Program
    {
        public static bool IsTest { get; set; } = false;
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddUniwikiClientHostServices();
            
            var host = builder.Build();
            await host.InitializeClient();

            await host.RunAsync();
        }
    }
}

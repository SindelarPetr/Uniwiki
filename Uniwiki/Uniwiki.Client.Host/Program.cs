using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Routing;
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
            
            foreach (var service in builder.Services)
            {
                Console.WriteLine(service.ServiceType.ToString());
            }

            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddUniwikiClientHostServices();
            
            var host = builder.Build();
            await host.InitializeClient();

            await host.RunAsync();
        }
    }
}

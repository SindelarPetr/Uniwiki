using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Uniwiki.Client.Host.Extensions;

namespace Uniwiki.Client.Host
{
    public class Program
    {
        public static bool IsTest { get; set; } = false;
        public static async Task Main(string[] args)
        {
            // Create the app builder. Uncover magic here https://github.com/dotnet/aspnetcore/blob/bbc116254accb4f279010afc1ceb08a5141d012b/src/Components/WebAssembly/WebAssembly/src/Hosting/WebAssemblyHostBuilder.cs
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Add root component
            builder.RootComponents.Add<App>("app"); 

            // Add Uniwiki services
            builder.Services.AddUniwikiClientHostServices();

            // On Development environment validate the DI scopes
            if (builder.HostEnvironment.IsDevelopment())
                builder.Services.BuildServiceProvider(true);

            Console.WriteLine("Client environment: " + builder.HostEnvironment.Environment);
            
            // Create the builder
            var host = builder.Build();

            // Initialize the client - thing, which should be run before start of the app
            await host.InitializeClient(builder.HostEnvironment.BaseAddress);

            // Run the app
            await host.RunAsync();
        }
    }
}

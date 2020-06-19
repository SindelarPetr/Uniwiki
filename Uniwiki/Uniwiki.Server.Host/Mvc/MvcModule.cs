using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared;
using Uniwiki.Server.Application.Services;

namespace Uniwiki.Server.Host.Mvc
{
    public class MvcModule
    {
        public Task Run()
        {
            var webHost = CreateHostBuilder().Build();
            //var initDataService = webHost.Services.GetService(typeof(IFakeDataInitializationService)) as IFakeDataInitializationService;
            //initDataService?.InitializeData();
            return webHost.StartAsync();
        }

        
        private IHostBuilder CreateHostBuilder() =>
    Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<MvcStartup>();
        }).ConfigureLogging(l => l.AddConsole());
    }
}



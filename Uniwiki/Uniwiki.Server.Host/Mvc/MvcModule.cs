using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Uniwiki.Server.Host.Mvc
{
    public class MvcModule
    {
        public void Run()
        {
            // Configure Logging
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                // Log start
                Log.Information("Application is starting");
                
                // Start the web host
                CreateHostBuilder()
                    .UseSerilog() // Use serilog instead of the built-in Microsoft logger
                    .Build().Run();
                
                // Log end
                Log.Information("Application gracefully finished");
            }
            catch (System.Exception ex)
            {
                // Log fatal error
                Log.Fatal(ex, "The application enoucntered an Error and shut down.");
            }
            finally
            {
                // Wait for all log messages to be written
                Log.CloseAndFlush();
            }

        }


        private IHostBuilder CreateHostBuilder()
        {
            return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSetting("https_port", "5001").UseStartup<MvcStartup>();
                })
                .ConfigureLogging(
                builder => {
                    builder.AddConsole();
                    }
                );
        }
    }
}



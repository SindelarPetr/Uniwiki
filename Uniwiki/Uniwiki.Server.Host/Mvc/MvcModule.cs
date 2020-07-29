using System.Diagnostics.Contracts;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Uniwiki.Server.Host.Mvc
{
    public class MvcModule
    {
        public void Run()
        {
            var rootDirectory = Path.GetDirectoryName(typeof(MvcModule).Assembly.Location) ?? string.Empty;
            
            Directory.SetCurrentDirectory(rootDirectory);

            // Configure Logging
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(configuration)
                .CreateLogger();

            try
            {
                // Log start
                Log.Information("Application is starting");
                
                // Start the web host
                CreateHostBuilder().Build().Run();
                
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

        public static IWebHostBuilder CreateHostBuilder()
        {
            var builder = new WebHostBuilder()
                .UseKestrel()
                //.UseContentRoot(Directory.GetCurrentDirectory()) // This line probably makes us troubles - maybe not?
                .UseWebRoot(Path.Combine("wwwroot")) // Set the web root to d
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                })
                .UseSerilog() // Use serilog for logging
                .UseStartup<MvcStartup>();

            return builder;
        }

        private static void ConfigureAppConfiguration(WebHostBuilderContext webHostBuilderContext, IConfigurationBuilder configurationBuilder)
        {
            var env = webHostBuilderContext.HostingEnvironment;
            Log.Logger.Information("The environment is: " + webHostBuilderContext.HostingEnvironment.EnvironmentName);

            configurationBuilder
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Add general configuration
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true) // Add environment-specific configuration
                .AddJsonFile("emailsettings.json"); // Add email configuration

            if (env.IsDevelopment())
            {
                var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                if (appAssembly != null)
                {
                    configurationBuilder.AddUserSecrets(appAssembly, optional: true);
                }
            }
            configurationBuilder.AddEnvironmentVariables();
        }
    }
}



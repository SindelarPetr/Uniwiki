using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Shared;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Host.Services;
using Uniwiki.Server.Host.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Shared.Configuration;

namespace Uniwiki.Server.Host.Mvc
{
    public class MvcStartup
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        public MvcStartup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSharedServices();
            services.AddHostServices(_loggerFactory);
            services.AddScoped<IMvcRequestExceptionHandlerService, MvcRequestExceptionHandlerService>();
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddTransient<IConfigureOptions<KestrelServerOptions>, KestrelServerOptionsSetup>(); // For kestrel

            // Add configuration
            var uniwikiConfiguration = new UniwikiConfiguration();
            _configuration.GetSection("Uniwiki").Bind(uniwikiConfiguration);
            services.AddSingleton(uniwikiConfiguration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP requestBase pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, ILogger<MvcStartup> logger)
        {
            logger.LogInformation($"Configuring server... Stage: '{env.EnvironmentName}', Content root: '{env.ContentRootPath}, Web root path: '{env.WebRootPath}', Web root exists: {env.WebRootFileProvider.GetDirectoryContents("/").Exists}, Application name: '{env.ApplicationName}'");

            using (var scope = serviceProvider.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<DataManipulationService>().InitializeFakeData().GetAwaiter().GetResult();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseBlazorFrameworkFiles();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}

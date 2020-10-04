using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Server.Appliaction;
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.ServerActions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Shared;

[assembly: InternalsVisibleTo("Uniwiki.Tests")]
[assembly: InternalsVisibleTo("Uniwiki.Server.Application.Tests")]
namespace Uniwiki.Server.Application
{
    public static class UniwikiServerApplicationServices
    {
        public static readonly InMemoryDatabaseRoot InMemoryDatabaseRoot = new InMemoryDatabaseRoot();

        public static void AddUniwikiServerApplicationServices(this IServiceCollection services, ILoggerFactory? loggerFactory = null)
        {
            services.AddServerApplication();
            services.AddUniwikiSharedServices();

            services.AddUniwikiServerPersistence(options => {
                if (loggerFactory != null)
                {
                    options.UseLoggerFactory(loggerFactory);
                }
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(
                    "Data Source=PSINDELAR01\\SQLEXPRESS01;Initial Catalog=UniwikiLocalDatabase;Integrated Security=True");
            }
                //options.UseInMemoryDatabase("InMemoryDB", InMemoryDatabaseRoot).EnableSensitiveDataLogging()
                );

            services.AddTransient<IEmailService, EmailService>(); // Transient, because it uses HttpContext inside
            services.AddScoped<IServerActionProvider, ServerActionProvider>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IInputValidationService, InputValidationService>();
            services.AddScoped<TextService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            services.AddTransient<DataManipulationService>();
            services.AddSingleton<IHashService, HashService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddSingleton<IApplicationHostEnvironment, ServerHostEnvironment>();
            services.AddScoped<EmailConfirmationSenderService, EmailConfirmationSenderService>();
            services.AddScoped<RecentCoursesService>();
            services.AddScoped<FetchPostsService>();
            services.AddScoped<PostFileService>();

            AddServerActions(services);
        }

        private static void AddServerActions(IServiceCollection collection)
        {
            // Get server action types
            var serverActionTypes = new ServerActionProvider().ProvideServerActions();

            // Add all server action types to service collection.
            foreach (var serverActionType in serverActionTypes)
            {
                collection.AddTransient(serverActionType);
            }
        }
    }
}
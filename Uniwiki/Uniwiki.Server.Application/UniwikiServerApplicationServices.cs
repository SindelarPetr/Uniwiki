using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Server.Appliaction;
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Shared;
using Uniwiki.Shared.Services;

[assembly: InternalsVisibleTo("Uniwiki.Tests")]
[assembly: InternalsVisibleTo("Uniwiki.Server.Application.Tests")]
namespace Uniwiki.Server.Application
{
    public static class UniwikiServerApplicationServices
    {
        public static void AddUniwikiServerApplicationServices(this IServiceCollection services)
        {
            services.AddServerApplication();
            services.AddUniwikiSharedServices();
            services.AddUniwikiServerPersistence();

            services.AddTransient<IEmailService, EmailService>(); // Transient, because it uses HttpContext inside
            services.AddScoped<IServerActionProvider, ServerActionProvider>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IInputValidationService, InputValidationService>();
            services.AddScoped<TextService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            services.AddTransient<IDataManipulationService, DataManipulationService>();
            services.AddSingleton<IHashService, HashService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddSingleton<IApplicationHostEnvironment, ServerHostEnvironment>();
            services.AddScoped<IEmailConfirmationSenderService, EmailConfirmationSenderService>();
            services.AddScoped<IRecentCoursesService, RecentCoursesService>();
            services.AddScoped<IPostCategoryService, PostCategoryService>();

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
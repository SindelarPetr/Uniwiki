﻿using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Server.Appliaction;
using Server.Appliaction.ServerActions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.InMemory;
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
            services.AddUniwikiServerPersistenceInMemory();

            services.AddTransient<IEmailService, EmailService>(); // Scoped, because it uses HttpContext inside
            services.AddScoped<IServerActionProvider, ServerActionProvider>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IInputValidationService, InputValidationService>();
            services.AddScoped<TextService>();
            services.AddScoped<TextServiceBase>(p => p.GetService<TextService>());
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            services.AddTransient<IFakeDataInitializationService, FakeDataInitializationService>();
            services.AddSingleton<IHashService, HashService>();
            services.AddScoped<ILoginService, LoginService>();

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
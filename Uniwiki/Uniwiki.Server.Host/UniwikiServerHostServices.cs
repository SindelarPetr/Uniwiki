﻿using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Server.Appliaction.Services.Abstractions;
using Server.Host;
using Uniwiki.Server.Application;
using Uniwiki.Server.Host.Mvc;
using Uniwiki.Server.Host.Services;
using Uniwiki.Server.Host.Services.Abstractions;

[assembly: InternalsVisibleTo("Uniwiki.Tests")]
namespace Uniwiki.Server.Host
{
    public static class UniwikiServerHostServices
    {
        public static void AddHostServices(this IServiceCollection services, ILoggerFactory loggerFactory = null)
        {
            services.AddServerHost();
            services.AddUniwikiServerApplicationServices(loggerFactory);
            services.AddHttpContextAccessor();
            services.AddScoped<IMvcProcessor, MvcProcessor>();
            services.AddSingleton<IRequestDeserializer, RequestDeserializer>();
            services.AddScoped<IUploadFileService, UploadFileService>();
            services.AddScoped<TextService>();
            services.AddSingleton<InputContextService>();
            
        }
    }
}

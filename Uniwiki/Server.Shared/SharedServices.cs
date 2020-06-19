using Microsoft.Extensions.DependencyInjection;
using Shared.Services;
using Uniwiki.Shared.Services;
using Uniwiki.Shared.Services.Abstractions;

namespace Shared
{
    public static class SharedServices
    {
        public static void AddSharedServices(this IServiceCollection services)
        {
            services.AddSingleton<ITimeService, TimeService>();
            services.AddSingleton<IStringStandardizationService, StringStandardizationService>();
        }
    }
}

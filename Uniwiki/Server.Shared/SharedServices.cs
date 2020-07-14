using Microsoft.Extensions.DependencyInjection;
using Shared.Services;
using System.Runtime.CompilerServices;
using Uniwiki.Shared.Services;
using Uniwiki.Shared.Services.Abstractions;

[assembly: InternalsVisibleTo("Shared.Tests")]
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

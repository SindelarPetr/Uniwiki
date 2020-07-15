using Microsoft.Extensions.DependencyInjection;
using Shared.Services;
using Shared.Services.Abstractions;
using System.Runtime.CompilerServices;
using Uniwiki.Shared.Services;

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

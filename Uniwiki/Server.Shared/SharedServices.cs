using Microsoft.Extensions.DependencyInjection;
using Shared.Services;
using Shared.Services.Abstractions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Shared.Tests")]
namespace Shared
{
    public static class SharedServices
    {
        public static void AddSharedServices(this IServiceCollection services)
        {
            services.AddSingleton<ITimeService, TimeService>();
            services.AddSingleton<StringStandardizationService>();
            services.AddSingleton<IFileHelperService, FileHelperService>();
        }
    }
}

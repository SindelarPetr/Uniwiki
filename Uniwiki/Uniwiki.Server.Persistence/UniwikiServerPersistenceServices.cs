using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Server.Persistence;
using Uniwiki.Server.Persistence.Services;

[assembly: InternalsVisibleTo("Uniwiki.Tests")]
[assembly: InternalsVisibleTo("Uniwiki.Server.Persistence.InMemory")]

namespace Uniwiki.Server.Persistence
{
    public static class UniwikiServerPersistenceServices
    {
        public static void AddUniwikiServerPersistence(this IServiceCollection services)
        {
            services.AddServerPersistence();
            services.AddScoped<TextService>();
        }
    }
}

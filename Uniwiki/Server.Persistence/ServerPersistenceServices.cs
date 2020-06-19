using Microsoft.Extensions.DependencyInjection;
using Server.Persistence.Services;

namespace Server.Persistence
{
    public static class ServerPersistenceServices
    {
        public static void AddServerPersistence(this IServiceCollection services)
        {
            services.AddScoped<PersistenceTextService>();
        }
    }
}

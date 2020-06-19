using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Server.Appliaction.ServerActions;
using Server.Appliaction.Services;

[assembly: InternalsVisibleTo("Uniwiki.Tests")]
namespace Server.Appliaction
{
    public static class ServerAppliactionServices
    {
        public static void AddServerApplication(this IServiceCollection collection)
        {
            collection.AddScoped<IServerActionProvider, ServerActionProvider>();
            collection.AddScoped<IServerActionResolver, ServerActionResolver>();
            collection.AddScoped<TextService>();
        }
    }
}

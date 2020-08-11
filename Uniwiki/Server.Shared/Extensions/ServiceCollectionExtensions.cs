using System;
using System.Linq;
using System.Reflection;

namespace Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //public static void AddAllSingletons(this IServiceCollection services)
        //{
        //    // Find all class types implementing IFakeRepository
        //    //foreach (var repositoryType in typeof(ServerPersistenceInMemoryServices).Assembly.GetTypes().Where(t => t.IsClass && typeof(IFakeRepository).IsAssignableFrom(t)))
        //    //{
        //    //    // Find the repository interface type
        //    //    var interfaceType = typeof(ServerPersistenceInMemoryServices).Assembly.GetTypes().FirstOrDefault(t => t.IsAssignableFrom(repositoryType));

        //    //    // If didnt find, throw error
        //    //    if (interfaceType == null)
        //    //        throw new Exception($"Initialization of Persistence Repositories failed. Could not find interface for the type {repositoryType.FullName}");

        //    //    // Add to services
        //    //    services.AddTransient(interfaceType, repositoryType);
        //    //}
        //}

        /// <summary>
        /// Adds all implementations to the service collection, which implement directly just 1 interface and implement the TService interface.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="assembly">The assembly from which the services should be added.</param>
        //public static void AddAllImplementations<TService>(Assembly assembly)
        //{
        //    // Find all interfaces, which are implemented just by one class
        //    //var interfaces = assembly.GetTypes().Where(t => t.IsInterface)

        //    // Get all classes, which are not abstract and implement the defined interface
        //    //var classes = assembly
        //    //    .GetTypes()
        //    //    .Where(t => t.IsClass)
        //    //    .Where(t => !t.IsAbstract)
        //    //    .Where(t => t(new TypeFilter((a, b) => true)).GetInterfaces()

        //}
    }
}

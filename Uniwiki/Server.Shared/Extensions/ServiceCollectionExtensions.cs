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
    }
}

using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Uniwiki.Tests.Extensions
{
    public static class ServiceProviderExtension
    {
        public static void InjectDependencies<T>(this IServiceProvider provider, T tObject)
        {
            var propertiesWithInjectAttribute = tObject
                .GetType()
                .GetProperties()
                .Where(p => p.GetCustomAttribute(typeof(InjectAttribute)) != null);

            foreach (var propertyInfo in propertiesWithInjectAttribute)
            {
                var dependency = provider.GetService(propertyInfo.PropertyType) ?? throw new Exception($"Could not find the required dependency of type: { propertyInfo.PropertyType.FullName } in object of type { tObject.GetType().FullName } for property of name { propertyInfo.Name }");
                propertyInfo.SetValue(tObject, dependency);
            }
        }
    }
}

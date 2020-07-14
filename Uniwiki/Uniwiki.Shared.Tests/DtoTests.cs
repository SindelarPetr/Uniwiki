using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Uniwiki.Shared.Tests
{
    [TestClass]
    public class DtoTests
    {
        private TypeInfo[] GetAllDtos() => typeof(RouteHelper).Assembly.DefinedTypes
            .Where(t => t.IsClass && t.Name.ToLower().EndsWith("dto")).ToArray();

        /// <summary>
        /// By using Newtonsoft.Json, this test became obsolete
        /// </summary>
        //[TestMethod]
        public void AllDtosHaveDefaultConstructor()
        {
            // Get all xxxDto types
            var dtos = GetAllDtos();

            // Check if each has a public default constructor
            foreach (var typeInfo in dtos)
            {
                if (!typeInfo.DeclaredConstructors.Any(c => c.IsPublic && c.GetParameters().Length == 0))
                {
                    Assert.Fail($"Found a type ({typeInfo.FullName}) without a public parameterless constructor:\n{typeInfo.AssemblyQualifiedName}");
                }
            }
        }

        /// <summary>
        /// By using Newtonsoft.Json, this test became obsolete
        /// </summary>
        // [TestMethod]
        public void AllDtosHavePublicSetterOnAllProperties()
        {
            // Get all xxxDto types
            var dtos = GetAllDtos();

            // Check if each has a public default constructor
            foreach (var typeInfo in dtos)
            {
                foreach (var property in typeInfo.DeclaredProperties)
                {
                    if(property.SetMethod == null || !property.SetMethod.IsPublic)
                        Assert.Fail($"Found a type ({typeInfo.FullName}) with a property ({ typeInfo.FullName + "." + property.Name}) a non-public setter:\n{typeInfo.AssemblyQualifiedName}");
                }
                
            }
        }

    }
}

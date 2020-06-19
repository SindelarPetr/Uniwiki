using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Appliaction.ServerActions
{
    public class ServerActionProvider : IServerActionProvider 
    {
        public Type[] ProvideServerActions()
        {
            // Type which marks Server Actions
            var type = typeof(IServerAction);

            // Get all types derived from IServerAction which are not abstract
            var serverActionTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).Where(p => type.IsAssignableFrom(p) && !p.IsAbstract).ToArray();

            return serverActionTypes;
        }

        /// <summary>
        /// Provides dictionary of all ServerActions. The key is type of corresponding request and value is the type of the server action.
        /// </summary>
        /// <returns>Dictionary scontaining serverActions.</returns>
        public Dictionary<Type, Type> ProvideRequestsAndServerActions()
        {
            var serverActionTypes = ProvideServerActions();
            return serverActionTypes.ToDictionary(t => t.BaseType.GetGenericArguments()[0], t => t);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Appliaction.ServerActions;

namespace Server.Application.Tests
{
    [TestClass]
    public class ServerActionResolverTests
    {
        
    }

    class FakeServerActionProvider : IServerActionProvider
    {
        private readonly IEnumerable<Type> _serverActionTypes;

        /// <summary>
        /// Fill in by the server actions which should be tested
        /// </summary>
        /// <param name="serverActionTypes"></param>
        public FakeServerActionProvider(params Type[] serverActionTypes)
        {
            _serverActionTypes = serverActionTypes;
            foreach (var serverAction in _serverActionTypes)
            {
                if (serverAction.BaseType == null || serverAction.BaseType.GetGenericArguments().Length != 1)
                {
                    Assert.Fail("Got a type, which does not seems to be a server action: " + serverAction.FullName);
                }
            }
        }

        public Type[] ProvideServerActions()
        {
            return _serverActionTypes.ToArray();
        }

        public Dictionary<Type, Type> ProvideRequestsAndServerActions()
        {
            return _serverActionTypes.ToDictionary(t => t.BaseType.GetGenericArguments()[0], t => t);
        }
    }

}

using System;
using System.Collections.Generic;

namespace Server.Appliaction.ServerActions
{
    public interface IServerActionProvider
    {
        Type[] ProvideServerActions();
        Dictionary<Type, Type> ProvideRequestsAndServerActions();
    }
}
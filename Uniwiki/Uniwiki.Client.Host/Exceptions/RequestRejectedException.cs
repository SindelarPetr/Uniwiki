using System;

namespace Uniwiki.Client.Host.Exceptions
{
    public class RequestRejectedException : Exception
    {
        public RequestRejectedException(string message) : base(message)
        {
            
        }
    }
}

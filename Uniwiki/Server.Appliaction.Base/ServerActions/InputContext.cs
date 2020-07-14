using System;
using System.Net;
using Shared;

namespace Server.Appliaction.ServerActions
{
    public class InputContext
    {
        public Guid? AccessToken { get; }
        public string RequestId { get; }
        public Language Language { get; }
        public string AppVersion { get; }
        public IPAddress IpAddress { get; }

        public InputContext(Guid? accessToken, string requestId, Language language, string appVersion, IPAddress ipAddress)
        {
            AccessToken = accessToken;
            RequestId = requestId;
            Language = language;
            AppVersion = appVersion;
            IpAddress = ipAddress;
        }
    }
}
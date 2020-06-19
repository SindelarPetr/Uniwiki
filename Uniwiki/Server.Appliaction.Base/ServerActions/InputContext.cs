using System;
using Shared;

namespace Server.Appliaction.ServerActions
{
    public class InputContext
    {
        public Guid? AccessToken { get; }
        public Guid RequestId { get; }
        public Language Language { get; }
        public string AppVersion { get; }

        public InputContext(Guid? accessToken, Guid requestId, Language language, string appVersion)
        {
            AccessToken = accessToken;
            RequestId = requestId;
            Language = language;
            AppVersion = appVersion;
        }
    }
}
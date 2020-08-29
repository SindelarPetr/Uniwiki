using System;
using System.Net;
using Shared;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;

namespace Server.Appliaction.ServerActions
{
    public class RequestContext
    {
        public string RequestId { get; }
        public LoginTokenModel? LoginToken { get; }
        public Guid? UserId => LoginToken?.ProfileId;
        public Language Language { get; }
        public AuthenticationLevel AuthenticationLevel { get; }
        public bool IsAuthenticated => UserId != null;
        public IPAddress IpAddress { get; }

        public RequestContext(LoginTokenModel? loginToken, AuthenticationLevel authenticationLevel, string requestId, Language language, IPAddress ipAddress)
        {
            LoginToken = loginToken;
            AuthenticationLevel = authenticationLevel;
            RequestId = requestId;
            Language = language;
            IpAddress = ipAddress;
        }
    }
}
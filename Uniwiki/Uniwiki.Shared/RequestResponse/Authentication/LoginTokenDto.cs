using System;
using Uniwiki.Shared.Attributes;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class LoginTokenDto
    {
        [DontLog]
        public Guid PrimaryTokenId { get; set; }
        [DontLog]
        public Guid SecondaryTokenId { get; set; }
        public DateTime Expiration { get; set; }

        public LoginTokenDto(Guid primaryTokenId, DateTime expiration, Guid secondaryTokenId)
        {
            PrimaryTokenId = primaryTokenId;
            Expiration = expiration;
            SecondaryTokenId = secondaryTokenId;
        }

        // For serialization by LocalStorageService
        public LoginTokenDto()
        {

        }
    }
}
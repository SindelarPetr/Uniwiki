using System;
using Uniwiki.Shared.Attributes;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class LoginTokenDto
    {
        public Guid ProfileId { get; }
        [DontLog]
        public Guid PrimaryTokenId { get; set; }
        [DontLog]
        public Guid SecondaryTokenId { get; set; }
        public DateTime Expiration { get; set; }

        public LoginTokenDto(Guid profileId, Guid primaryTokenId, DateTime expiration, Guid secondaryTokenId)
        {
            ProfileId = profileId;
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
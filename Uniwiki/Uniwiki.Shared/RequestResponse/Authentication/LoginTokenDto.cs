using System;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class LoginTokenDto
    {
        public Guid PrimaryTokenId { get; set; }
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
using System;

namespace Uniwiki.Server.Persistence.Models.Authentication
{
    public class LoginTokenModel
    {
        public Guid PrimaryTokenId { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        public DateTime Expiration { get; protected set; }
        public Guid SecondaryTokenId { get; protected set; }

        public LoginTokenModel()
        {

        }

        public LoginTokenModel(Guid primaryTokenId, ProfileModel profile, DateTime creationTime, DateTime expiration, Guid secondaryTokenId)
        {
            PrimaryTokenId = primaryTokenId;
            Profile = profile;
            CreationTime = creationTime;
            Expiration = expiration;
            SecondaryTokenId = secondaryTokenId;
        }
    }
}
using System;

namespace Uniwiki.Server.Persistence.Models.Authentication
{
    public sealed class LoginTokenModel
    {
        public Guid PrimaryTokenId { get; }
        public ProfileModel Profile { get; }
        public DateTime CreationTime { get; }
        public DateTime Expiration { get; }
        public Guid SecondaryTokenId { get; }

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
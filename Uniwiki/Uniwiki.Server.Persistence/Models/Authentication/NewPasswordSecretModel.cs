using System;

namespace Uniwiki.Server.Persistence.Models.Authentication
{
    public class NewPasswordSecretModel
    {
        public ProfileModel Profile { get; protected set; }
        public Guid Secret { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        public DateTime ExpirationTime { get; protected set; }
        public bool IsValid { get; protected set; }

        internal NewPasswordSecretModel()
        {

        }

        internal NewPasswordSecretModel(ProfileModel profile, Guid secret, DateTime creationTime, DateTime expirationTime, bool isValid)
        {
            Profile = profile;
            Secret = secret;
            CreationTime = creationTime;
            ExpirationTime = expirationTime;
            IsValid = isValid;
        }

        internal void Invalidate()
        {
            IsValid = false;
        }
    }
}
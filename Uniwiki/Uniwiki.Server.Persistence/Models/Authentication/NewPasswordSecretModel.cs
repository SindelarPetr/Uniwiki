using System;

namespace Uniwiki.Server.Persistence.Models.Authentication
{
    public sealed class NewPasswordSecretModel
    {
        public ProfileModel Profile { get; }
        public Guid Secret { get; }
        public DateTime CreationTime { get; }
        public DateTime ExpirationTime { get; }
        public bool IsValid { get; private set; }

        public NewPasswordSecretModel(ProfileModel profile, Guid secret, DateTime creationTime, DateTime expirationTime, bool isValid)
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
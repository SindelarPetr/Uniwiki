using System;

namespace Uniwiki.Server.Persistence.Models.Authentication
{
    public sealed class EmailConfirmationSecretModel
    {
        public ProfileModel Profile { get; }
        public Guid Secret { get; }
        public DateTime CreationTime { get; }
        public bool IsValid { get; private set; }

        public EmailConfirmationSecretModel(ProfileModel profile, Guid secret, DateTime creationTime, bool isValid)
        {
            Profile = profile;
            Secret = secret;
            CreationTime = creationTime;
            IsValid = isValid;
        }

        public void Invalidate()
        {
            IsValid = false;
        }
    }
}
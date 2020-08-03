using System;

namespace Uniwiki.Server.Persistence.Models.Authentication
{
    public class EmailConfirmationSecretModel
    {
        public ProfileModel Profile { get; protected set; }
        public Guid Secret { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        public bool IsValid { get; protected set; }

        internal EmailConfirmationSecretModel()
        {

        }

        internal EmailConfirmationSecretModel(ProfileModel profile, Guid secret, DateTime creationTime, bool isValid)
        {
            Profile = profile;
            Secret = secret;
            CreationTime = creationTime;
            IsValid = isValid;
        }

        internal void Invalidate()
        {
            IsValid = false;
        }
    }
}
using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class NewPasswordSecretModel : IIdModel<Guid>
    {
        public ProfileModel Profile { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        public DateTime ExpirationTime { get; protected set; }
        public bool IsValid { get; protected set; }
        public Guid Id { get; protected set; }
        public Guid Secret { get; protected set; }

        internal NewPasswordSecretModel(Guid id, ProfileModel profile, Guid secret, DateTime creationTime, DateTime expirationTime, bool isValid)
        {
            Id = id;
            Profile = profile;
            Secret = secret;
            CreationTime = creationTime;
            ExpirationTime = expirationTime;
            IsValid = isValid;
        }

        protected NewPasswordSecretModel()
        {

        }

        internal void Invalidate()
        {
            IsValid = false;
        }
    }
}
using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class NewPasswordSecretModel : ModelBase<Guid>
    {
        public Guid ProfileId { get; protected set; }
        public ProfileModel Profile { get; protected set; } = null!;
        public DateTime CreationTime { get; protected set; }
        public DateTime ExpirationTime { get; protected set; }
        public bool IsValid { get; protected set; }
        public Guid Secret { get; protected set; }

        internal NewPasswordSecretModel(Guid id, ProfileModel profile, Guid secret, DateTime creationTime, DateTime expirationTime, bool isValid)
            : base(id)
        {
            ProfileId = profile.Id;
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
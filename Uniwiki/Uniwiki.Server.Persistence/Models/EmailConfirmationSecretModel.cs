using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class EmailConfirmationSecretModel : ModelBase<Guid>
    {
        public Guid ProfileId { get; protected set; }
        public ProfileModel Profile { get; protected set; } = null!;
        public DateTime CreationTime { get; protected set; }
        public bool IsValid { get; protected set; }
        public Guid Secret { get; protected set; }

        internal EmailConfirmationSecretModel(Guid id, ProfileModel profile, Guid secret, DateTime creationTime, bool isValid)
            : base(id)
        {
            ProfileId = profile.Id;
            Profile = profile;
            Secret = secret;
            CreationTime = creationTime;
            IsValid = isValid;
        }

        protected EmailConfirmationSecretModel()
        {

        }

        internal void Invalidate()
        {
            IsValid = false;
        }
    }
}
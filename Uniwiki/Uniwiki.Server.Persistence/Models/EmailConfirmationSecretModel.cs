using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class EmailConfirmationSecretModel : IIdModel<Guid>
    {
        public ProfileModel Profile { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        public bool IsValid { get; protected set; }
        public Guid Id { get; set; }
        public Guid Secret { get; protected set; }

        internal EmailConfirmationSecretModel(Guid id, ProfileModel profile, Guid secret, DateTime creationTime, bool isValid)
        {
            Id = id;
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
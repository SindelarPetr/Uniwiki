using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class EmailConfirmationSecretModel : IIdModel<Guid>
    {
        public ProfileModel Profile { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        public bool IsValid { get; protected set; }
        public Guid Id { get; protected set; }

        internal EmailConfirmationSecretModel(ProfileModel profile, Guid id, DateTime creationTime, bool isValid)
        {
            Profile = profile;
            Id = id;
            CreationTime = creationTime;
            IsValid = isValid;
        }

        internal EmailConfirmationSecretModel()
        {

        }

        internal void Invalidate()
        {
            IsValid = false;
        }
    }
}
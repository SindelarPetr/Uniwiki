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
        public Guid Id { get; }

        internal NewPasswordSecretModel(ProfileModel profile, Guid id, DateTime creationTime, DateTime expirationTime, bool isValid)
        {
            Profile = profile;
            Id = id;
            CreationTime = creationTime;
            ExpirationTime = expirationTime;
            IsValid = isValid;
        }

        internal NewPasswordSecretModel()
        {

        }

        internal void Invalidate()
        {
            IsValid = false;
        }
    }
}
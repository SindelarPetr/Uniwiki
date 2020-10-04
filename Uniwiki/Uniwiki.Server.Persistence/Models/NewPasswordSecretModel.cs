using System;
using Uniwiki.Server.Persistence.Models.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class NewPasswordSecretModel : ModelBase<Guid>
    {
        public Guid ProfileId { get; set; }
        public ProfileModel Profile { get; set; } = null!;
        public DateTime CreationTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public bool IsValid { get; set; }
        public Guid Secret { get; set; }

        internal NewPasswordSecretModel(Guid id, Guid profileId, Guid secret, DateTime creationTime, DateTime expirationTime, bool isValid)
            : base(id)
        {
            ProfileId = profileId;
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
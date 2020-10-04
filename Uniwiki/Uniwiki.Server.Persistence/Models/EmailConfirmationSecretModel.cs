using System;
using Toolbelt.ComponentModel.DataAnnotations.Schema.V5;
using Uniwiki.Server.Persistence.Models.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class EmailConfirmationSecretModel : ModelBase<Guid>
    {
        public Guid ProfileId { get; set; }
        public ProfileModel Profile { get; set; } = null!;
        public DateTime CreationTime { get; set; }
        public bool IsValid { get; set; }
        [IndexColumn]
        public Guid Secret { get; set; }

        internal EmailConfirmationSecretModel(Guid id, Guid profileId, Guid secret, DateTime creationTime, bool isValid)
            : base(id)
        {
            ProfileId = profileId;
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
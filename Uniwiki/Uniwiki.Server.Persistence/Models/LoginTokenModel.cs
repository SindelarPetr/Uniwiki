using System;
using Toolbelt.ComponentModel.DataAnnotations.Schema.V5;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class LoginTokenModel : ModelBase<Guid>
    {
        [IndexColumn]
        public Guid PrimaryTokenId { get; protected set; }
        public Guid ProfileId { get; protected set; }
        public ProfileModel Profile { get; protected set; } = null!;
        [IndexColumn]
        public DateTime CreationTime { get; protected set; }
        public DateTime Expiration { get; protected set; }
        [IndexColumn]
        public Guid SecondaryTokenId { get; protected set; }

        internal LoginTokenModel(Guid id, Guid primaryTokenId, Guid secondaryTokenId, Guid profileId, DateTime creationTime, DateTime expiration)
            : base(id)
        {
            PrimaryTokenId = primaryTokenId;
            ProfileId = profileId;
            CreationTime = creationTime;
            Expiration = expiration;
            SecondaryTokenId = secondaryTokenId;
        }
    }
}
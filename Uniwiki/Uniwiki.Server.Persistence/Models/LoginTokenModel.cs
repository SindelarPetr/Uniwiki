using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class LoginTokenModel : IIdModel<Guid>
    {
        public Guid Id { get; protected set; }
        public Guid PrimaryTokenId { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        public DateTime Expiration { get; protected set; }
        public Guid SecondaryTokenId { get; protected set; }

        public LoginTokenModel(Guid id, Guid primaryTokenId, Guid secondaryTokenId, ProfileModel profile, DateTime creationTime, DateTime expiration)
        {
            Id = id;
            PrimaryTokenId = primaryTokenId;
            Profile = profile;
            CreationTime = creationTime;
            Expiration = expiration;
            SecondaryTokenId = secondaryTokenId;
        }

        protected LoginTokenModel()
        {

        }
    }
}
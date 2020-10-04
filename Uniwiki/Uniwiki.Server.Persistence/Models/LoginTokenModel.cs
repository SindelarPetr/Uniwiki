using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toolbelt.ComponentModel.DataAnnotations.Schema.V5;
using Uniwiki.Server.Persistence.Models.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class LoginTokenModel : ModelBase<Guid>
    {
        public class Map : IEntityTypeConfiguration<LoginTokenModel>
        {
            public void Configure(EntityTypeBuilder<LoginTokenModel> builder) =>
                builder
                    .HasMany(m => m.PostFileDownloads)
                    .WithOne(d => d.Token)
                    .OnDelete(DeleteBehavior.NoAction);
        }

        [IndexColumn]
        public Guid PrimaryTokenId { get; set; }
        public Guid ProfileId { get; set; }
        public ProfileModel Profile { get; set; } = null!;
        [IndexColumn]
        public DateTime CreationTime { get; set; }
        public DateTime Expiration { get; set; }
        [IndexColumn]
        public Guid SecondaryTokenId { get; set; }

        public ICollection<PostFileDownloadModel> PostFileDownloads { get; set; } = null!;
        internal LoginTokenModel(Guid id, Guid primaryTokenId, Guid secondaryTokenId, Guid profileId, DateTime creationTime, DateTime expiration)
            : base(id)
        {
            PrimaryTokenId = primaryTokenId;
            ProfileId = profileId;
            CreationTime = creationTime;
            Expiration = expiration;
            SecondaryTokenId = secondaryTokenId;
            PostFileDownloads = new List<PostFileDownloadModel>();
        }
    }
}
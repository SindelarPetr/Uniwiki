using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.ModelIds;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostLikeModelMap : ModelMapBase<PostLikeModel, PostLikeModelId>
    {
        public PostLikeModelMap() : base("PostLikes")
        {
        }

        public override void Map(EntityTypeBuilder<PostLikeModel> builder)
        {
            base.Map(builder);

            builder.HasKey(m => new PostLikeModelId(m.PostId, m.ProfileId));
            builder.HasOne(m => m.Post).WithMany().HasForeignKey(m => m.PostId);
            builder.HasOne(m => m.Profile).WithMany().HasForeignKey(m => m.ProfileId);
        }
    }
}
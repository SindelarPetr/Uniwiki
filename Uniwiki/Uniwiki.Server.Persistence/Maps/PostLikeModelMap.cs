using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Uniwiki.Server.Persistence.Maps.Base;
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

            builder.HasKey(e => new PostLikeModelId(e.PostId, e.ProfileId));
        }
    }
}
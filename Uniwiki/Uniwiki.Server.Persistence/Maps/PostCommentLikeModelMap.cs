using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostCommentLikeModelMap : ModelMapBase<PostCommentLikeModel, PostCommentLikeModelId>
    {
        public PostCommentLikeModelMap() : base("PostCommentLikes")
        {
        }

        public override void Map(EntityTypeBuilder<PostCommentLikeModel> builder)
        {
            base.Map(builder);

            builder.HasKey(e => new PostCommentLikeModelId(e.CommentId, e.ProfileId));
        }
    }
}
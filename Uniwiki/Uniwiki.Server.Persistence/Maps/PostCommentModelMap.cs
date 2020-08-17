using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostCommentModelMap : RemovableModelMapBase<PostCommentModel, Guid>
    {
        public PostCommentModelMap() : base("PostComments")
        {

        }

        public override void Map(EntityTypeBuilder<PostCommentModel> builder)
        {
            base.Map(builder);

            builder
                .HasOne(m => m.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(m => m.PostId);

            builder
                .HasOne(m => m.Profile)
                .WithMany()
                .HasForeignKey(m => m.ProfileId);

            builder
                .HasMany(m => m.Likes)
                .WithOne(l => l.Comment)
                .HasForeignKey(l => l.CommentId);
        }
    }
}
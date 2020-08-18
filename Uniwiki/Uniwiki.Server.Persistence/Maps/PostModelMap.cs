using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostModelMap : RemovableModelMapBase<PostModel, Guid>
    {
        public PostModelMap() : base("Posts")
        {
        }

        public override void Map(EntityTypeBuilder<PostModel> builder)
        {
            base.Map(builder);

            builder.HasOne(m => m.Author).WithMany().HasForeignKey(m => m.AuthorId);
            builder.HasOne(m => m.Course).WithMany(c => c.Posts).HasForeignKey(m => m.CourseId);
            builder.HasIndex(m => m.CreationTime);
        }
    }
}

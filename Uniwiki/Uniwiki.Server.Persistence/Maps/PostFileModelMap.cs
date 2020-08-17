using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostFileModelMap : ModelMapBase<PostFileModel, Guid>
    {
        public PostFileModelMap() : base("PostFiles")
        {
        }

        public override void Map(EntityTypeBuilder<PostFileModel> builder)
        {
            base.Map(builder);

            builder.HasOne(m => m.Profile).WithMany().HasForeignKey(m => m.ProfileId);
            builder.HasOne(m => m.Course).WithMany().HasForeignKey(m => m.CourseId);
        }
    }
}
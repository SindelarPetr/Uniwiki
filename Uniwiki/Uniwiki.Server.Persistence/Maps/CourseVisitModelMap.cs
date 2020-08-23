using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class CourseVisitModelMap : ModelMapBase<CourseVisitModel, Guid>
    {
        public CourseVisitModelMap() : base("CourseVisits")
        {

        }

        public override void Map(EntityTypeBuilder<CourseVisitModel> builder)
        {
            base.Map(builder);

            builder.HasOne(m => m.Course).WithMany().HasForeignKey(m => m.CourseId);
            builder.HasOne(m => m.Profile).WithMany().HasForeignKey(m => m.ProfileId);
        }
    }
}
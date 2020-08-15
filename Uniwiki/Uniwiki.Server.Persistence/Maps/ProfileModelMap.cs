using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Server.Persistence.Models
{
    public class ProfileModelMap : ModelMapBase<ProfileModel, Guid>
    {
        public ProfileModelMap() : base("Profiles")
        {
        }

        public override void Map(EntityTypeBuilder<ProfileModel> builder)
        {
            base.Map(builder);

            builder.HasOne(m => m.HomeFaculty).WithMany();
            builder.HasMany(m => m.Feedbacks).WithOne();
            builder.HasMany(m => m.RecentCourses).WithOne();
        }
    }
}

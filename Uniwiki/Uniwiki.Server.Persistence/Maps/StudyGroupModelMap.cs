using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class StudyGroupModelMap : RemovableModelMapBase<StudyGroupModel, Guid>
    {
        public StudyGroupModelMap() : base("StudyGroups")
        {

        }

        public override void Map(EntityTypeBuilder<StudyGroupModel> builder)
        {
            base.Map(builder);

            builder.HasOne(m => m.Profile).WithMany();
            builder.HasMany(m => m.Courses).WithOne();
        }
    }
}
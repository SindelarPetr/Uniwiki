using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Uniwiki.Server.Persistence.Maps.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class UniversityModelMap : RemovableModelMapBase<UniversityModel, Guid>
    {
        public UniversityModelMap() : base("Universities")
        {

        }

        public override void Map(EntityTypeBuilder<UniversityModel> builder)
        {
            base.Map(builder);

            builder.HasMany(m => m.StudyGroups).WithOne(m => m.University);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class CourseModelMap : RemovableModelMapBase<CourseModel, Guid>
    {
        public CourseModelMap() : base("Courses")
        {

        }

        public override void Map(EntityTypeBuilder<CourseModel> builder)
        {
            base.Map(builder);

            builder
                .HasOne(m => m.Author)
                .WithMany()
                .HasForeignKey(m => m.AuthorId);

            // Index over url address
            builder.HasIndex(m => new { courseUrl = m.Url, m.StudyGroupUrl, m.UniversityUrl });
        }
    }
}
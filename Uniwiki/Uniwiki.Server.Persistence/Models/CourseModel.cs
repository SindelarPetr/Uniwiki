using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uniwiki.Server.Persistence.Models.Base;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Shared;

namespace Uniwiki.Server.Persistence.Models
{
    public class CourseModel : ModelBase<Guid>
    {
        public class Map : IEntityTypeConfiguration<CourseModel>
        {
            public void Configure(EntityTypeBuilder<CourseModel> builder)
            {
                builder
                    .HasMany(m => m.CourseVisits)
                    .WithOne(v => v.Course)
                    .OnDelete(DeleteBehavior.Cascade);

                builder
                    .HasIndex(e => new {e.Url, e.StudyGroupUrl, e.UniversityUrl});
            }
        }

        [MaxLength(Constants.Validations.CourseCodeMaxLength)]
        public string Code { get; set; } = null!;
        [MaxLength(Constants.Validations.CourseCodeMaxLength)]
        public string CodeStandardized { get; set; } = null!;
        [MaxLength(Constants.Validations.CourseNameMaxLength)]
        public string LongName { get; set; } = null!;
        [MaxLength(Constants.Validations.CourseNameMaxLength)]
        public string FullNameStandardized { get; set; } = null!;
        public Guid StudyGroupId { get; set; }
        public StudyGroupModel StudyGroup { get; set; } = null!;
        public Guid? AuthorId { get; set; }
        public ProfileModel? Author { get; set; } = null!;
        [MaxLength(Constants.Validations.UrlMaxLength)]
        public string Url { get; set; } = null!;
        [MaxLength(Constants.Validations.UrlMaxLength)]
        public string StudyGroupUrl { get; set; } = null!;
        [MaxLength(Constants.Validations.UrlMaxLength)]
        public string UniversityUrl { get; set; } = null!;

        public ICollection<PostModel> Posts { get; set; } = null!;
        public ICollection<CourseVisitModel> CourseVisits { get; set; } = null!;

        public CourseModel(
            Guid id,
            string? code,
            string? codeStandardized,
            string fullname,
            string fullnameStandardized,
            Guid? authorId,
            Guid studyGroupId,
            string universityUrl,
            string url,
            string studyGroupUrl)
            : base(id)
        {
            Code = code ?? String.Empty;
            CodeStandardized = codeStandardized ?? String.Empty;
            LongName = fullname;
            FullNameStandardized = fullnameStandardized;
            AuthorId = authorId;
            StudyGroupId = studyGroupId;
            Url = url;
            StudyGroupUrl = studyGroupUrl;
            UniversityUrl = universityUrl;
            Posts = new List<PostModel>();
            CourseVisits = new List<CourseVisitModel>();
        }

        protected CourseModel()
        {

        }
    }
}
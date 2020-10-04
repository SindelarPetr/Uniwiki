using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shared;
using Toolbelt.ComponentModel.DataAnnotations.Schema.V5;
using Uniwiki.Server.Persistence.Models.Base;
using Uniwiki.Shared;

namespace Uniwiki.Server.Persistence.Models
{
    public class StudyGroupModel : ModelBase<Guid>
    {
        public Guid UniversityId { get; set; }
        public UniversityModel University { get; set; } = null!;
        [MaxLength(Constants.Validations.StudyGroupShortcutMaxLength)]
        public string ShortName { get; set; } = null!;
        [MaxLength(Constants.Validations.StudyGroupNameMaxLength)]
        public string LongName { get; set; } = null!;
        [MaxLength(Constants.Validations.StudyGroupNameMaxLength)]
        public string LongNameForSearching { get; set; } = null!;
        [IndexColumn]
        public string Url { get; set; } = null!;

        public ICollection<CourseModel> Courses { get; set; } = null!;
        public Language PrimaryLanguage { get; set; }

        public StudyGroupModel(Guid id, Guid universityId, string shortName, string longName, string url,
            Language primaryLanguage)
            : base(id)
        {
            UniversityId = universityId;
            ShortName = shortName;
            LongName = longName;
            LongNameForSearching = longName.ToLower();
            Url = url;
            PrimaryLanguage = primaryLanguage;
            Courses = new List<CourseModel>();
        }

        protected StudyGroupModel()
        {

        }
    }
}
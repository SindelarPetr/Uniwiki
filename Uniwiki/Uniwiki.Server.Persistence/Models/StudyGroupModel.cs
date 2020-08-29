using System;
using System.Collections.Generic;
using Shared;
using Toolbelt.ComponentModel.DataAnnotations.Schema.V5;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class StudyGroupModel : RemovableModelBase<Guid>
    {
        public Guid UniversityId { get; protected set; }
        public UniversityModel University { get; protected set; } = null!;
        public string ShortName { get; protected set; } = null!;
        public string ShortNameForSearching { get; protected set; } = null!;
        public string LongName { get; protected set; } = null!;
        public string LongNameForSearching { get; protected set; } = null!;
        [IndexColumn]
        public string Url { get; protected set; } = null!;
        public Guid ProfileId { get; protected set; }
        public ProfileModel Profile { get; protected set; } = null!;
        public ICollection<CourseModel> Courses { get; protected set; }
        = new List<CourseModel>();
        public Language PrimaryLanguage { get; protected set; }

        internal StudyGroupModel(Guid id, Guid universityId, string shortName, string longName, string url,
            Guid profileId, Language primaryLanguage, bool isRemoved)
            : base(isRemoved, id)
        {
            UniversityId = universityId;
            ShortName = shortName;
            LongName = longName;
            LongNameForSearching = longName.ToLower();
            Url = url;
            ProfileId = profileId;
            PrimaryLanguage = primaryLanguage;
        }

        protected StudyGroupModel()
        {

        }
    }
}
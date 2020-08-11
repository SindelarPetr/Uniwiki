using System;
using System.Collections.Generic;
using Shared;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class StudyGroupModel : IRemovableModel, IIdModel<Guid>
    {
        public UniversityModel University { get; protected set; }
        public string ShortName { get; protected set; }
        public string LongName { get; protected set; }
        public string Url { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public IEnumerable<CourseModel> Courses { get; protected set; }
        public Language PrimaryLanguage { get; protected set; }
        bool IRemovableModel.IsRemoved { get; set; }
        public Guid Id { get; protected set; }

        public StudyGroupModel(Guid id, UniversityModel university, string shortName, string longName, string url,
            ProfileModel profile, Language primaryLanguage, bool isRemoved)
        {
            Id = id;
            University = university;
            ShortName = shortName;
            LongName = longName;
            Url = url;
            Profile = profile;
            PrimaryLanguage = primaryLanguage;
            ((IRemovableModel)this).IsRemoved = isRemoved;
        }

        protected StudyGroupModel()
        {

        }
    }
}
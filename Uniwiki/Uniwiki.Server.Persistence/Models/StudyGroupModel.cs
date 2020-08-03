using System;
using System.Collections.Generic;
using Shared;

namespace Uniwiki.Server.Persistence.Models
{
    public class StudyGroupModel
    {
        public UniversityModel University { get; protected set; }
        public string ShortName { get; protected set; }
        public string LongName { get; protected set; }
        public string Url { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public Guid Id { get; protected set; }
        public IEnumerable<CourseModel> Courses { get; protected set; }

        internal StudyGroupModel()
        {

        }

        internal StudyGroupModel(Guid id, UniversityModel university, string shortName, string longName, string url,
            ProfileModel profile, Language primaryLanguage, IEnumerable<CourseModel> courses)
        {
            Id = id;
            University = university;
            ShortName = shortName;
            LongName = longName;
            Url = url;
            Profile = profile;
            PrimaryLanguage = primaryLanguage;
            Courses = courses;
        }

        public Language PrimaryLanguage { get; }
    }
}
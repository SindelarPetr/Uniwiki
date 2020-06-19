using System;
using System.Collections.Generic;
using Shared;

namespace Uniwiki.Server.Persistence.Models
{
    public class StudyGroupModel
    {
        public UniversityModel University { get; }
        public string ShortName { get; }
        public string LongName { get; }
        public string Url { get; }
        public ProfileModel Profile { get; }
        public Guid Id { get; }
        public IEnumerable<CourseModel> Courses { get; }

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
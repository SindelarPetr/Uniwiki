using System;

namespace Uniwiki.Shared.ModelDtos
{
    public class CourseDto
    {
        public Guid Id { get; }
        public string LongName { get; }
        public string ShortName { get; }
        public string Url { get; }
        public string StudyGroupLongName { get; }
        public string StudyGroupUrl { get; }
        public string UniversityShortName { get; }
        public string UniversityUrl { get; }

        public CourseDto(Guid id, string longName, string shortName, string url, string studyGroupLongName, string studyGroupUrl, string universityShortName, string universityUrl)
        {
            Id = id;
            LongName = longName;
            ShortName = shortName;
            Url = url;
            StudyGroupLongName = studyGroupLongName;
            StudyGroupUrl = studyGroupUrl;
            UniversityShortName = universityShortName;
            UniversityUrl = universityUrl;
        }
    }
}

using System;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class HomeStudyGroupDto
    {
        public Guid StudyGroupId { get; set; }
        public string ShortName { get; set; } = null!;
        public string LongName { get; set; } = null!;
        public string UniversityShortName { get; set; } = null!;
        public string UniversityLongName { get; set; } = null!;
        public string StudyGroupUrl { get; set; } = null!;
        public string UniversityUrl { get; set; } = null!;
        public Guid UniversityId { get; set; }

        public HomeStudyGroupDto(Guid studyGroupId, string shortName, string longName, string universityShortName, string universityLongName, string studyGroupUrl, string universityUrl, Guid universityId)
        {
            StudyGroupId = studyGroupId;
            ShortName = shortName;
            LongName = longName;
            UniversityShortName = universityShortName;
            UniversityLongName = universityLongName;
            StudyGroupUrl = studyGroupUrl;
            UniversityUrl = universityUrl;
            UniversityId = universityId;
        }

        public HomeStudyGroupDto()
        {
            
        }
    }
}
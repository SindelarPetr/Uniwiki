using System;

namespace Uniwiki.Shared.ModelDtos
{
    public class StudyGroupDto
    {
        public Guid Id { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string Url { get; set; }
        public string UniversityLongName { get; set; }
        public string UniversityShortName { get; set; }
        public string UniversityUrl { get; set; }
        public Guid UniversityId { get; set; }

        public StudyGroupDto(Guid id, string shortName, string longName, string url, string universityLongName, string universityShortName, string universityUrl, Guid universityId)
        {
            Id = id;
            ShortName = shortName;
            LongName = longName;
            Url = url;
            UniversityLongName = universityLongName;
            UniversityShortName = universityShortName;
            UniversityUrl = universityUrl;
            UniversityId = universityId;
        }
    }
}

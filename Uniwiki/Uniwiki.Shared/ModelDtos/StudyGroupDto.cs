using System;

namespace Uniwiki.Shared.ModelDtos
{
    public class StudyGroupDto
    {
        public Guid Id { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string Url { get; set; }
        public UniversityDto University { get; set; }

        public StudyGroupDto(Guid id, string shortName, string longName, string url, UniversityDto university)
        {
            Id = id;
            ShortName = shortName;
            LongName = longName;
            Url = url;
            University = university;
        }
    }
}

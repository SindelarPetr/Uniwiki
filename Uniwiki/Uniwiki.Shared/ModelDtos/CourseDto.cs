using System;

namespace Uniwiki.Shared.ModelDtos
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string Url { get; set; }
        public StudyGroupDto StudyGroup { get; set; }
        public UniversityDto University { get; set; }

        public CourseDto(Guid id, string longName, string shortName, string url, StudyGroupDto studyGroup, UniversityDto university)
        {
            Id = id;
            LongName = longName;
            ShortName = shortName;
            Url = url;
            StudyGroup = studyGroup;
            University = university;
        }
    }
}

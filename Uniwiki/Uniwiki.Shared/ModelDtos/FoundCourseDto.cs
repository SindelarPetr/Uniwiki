using System;
using System.Collections.Generic;
using System.Text;

namespace Uniwiki.Shared.ModelDtos
{
    public class FoundCourseDto
    {
        public Guid StudyGroupId { get; }
        public Guid Id { get; }
        public string FullUrl { get; }
        public string UniversityAndFaculty { get; }
        public string Name { get; }
        public string? Code { get; }

        public FoundCourseDto(Guid courseId, string fullUrl, string universityAndFaculty, string? code, string name, Guid studyGroupId)
        {
            Id = courseId;
            FullUrl = fullUrl;
            UniversityAndFaculty = universityAndFaculty;
            Code = code;
            Name = name;
            StudyGroupId = studyGroupId;
        }
    }
}

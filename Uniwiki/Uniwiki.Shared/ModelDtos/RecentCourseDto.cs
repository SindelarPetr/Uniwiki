using System;
using System.Collections.Generic;
using System.Text;

namespace Uniwiki.Shared.ModelDtos
{
    public class RecentCourseDto
    {
        public Guid Id { get; set; }
        public string CourseUrl { get; set; }
        public string CourseLongName { get; set; }
        public string? CourseCode { get; set; }
        public string StudyGroupUrl { get; set; }
        public string StudyGroupLongName { get; set; }
        public string UniversityUrl { get; set; }
        public string UniversityShortName { get; set; }

        public RecentCourseDto(string courseLongName, string? courseCode, string studyGroupUrl, string studyGroupLongName, string universityUrl, string universityShortName, Guid id, string courseUrl)
        {
            CourseLongName = courseLongName;
            CourseCode = courseCode;
            StudyGroupUrl = studyGroupUrl;
            StudyGroupLongName = studyGroupLongName;
            UniversityUrl = universityUrl;
            UniversityShortName = universityShortName;
            Id = id;
            CourseUrl = courseUrl;
        }

        public RecentCourseDto()
        {

        }
    }
}

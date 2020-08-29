using System;
using System.Collections.Generic;
using System.Text;

namespace Uniwiki.Shared.ModelDtos
{
    public class RecentCourseDto
    {
        public Guid Id { get; }
        public string CourseUrl { get; }
        public string CourseLongName { get; }
        public string? CourseCode { get; }
        public string StudyGroupUrl { get; }
        public string StudyGroupLongName { get; }
        public string UniversityUrl { get; }
        public string UniversityShortName { get; }

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
    }
}

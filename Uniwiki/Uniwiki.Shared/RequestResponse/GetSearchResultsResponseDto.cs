using System;
using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    //public class CourseItemDto
    //{
    //    public CourseItemDto(Guid id, string name, string? code, string url, string studyGroupUrl, string universityUrl, string universityShortName, string studyGroupLongName)
    //    {
    //        Id = id;
    //        Name = name;
    //        Code = code;
    //        Url = url;
    //        StudyGroupUrl = studyGroupUrl;
    //        UniversityUrl = universityUrl;
    //        UniversityShortName = universityShortName;
    //        StudyGroupLongName = studyGroupLongName;
    //    }

    //    public Guid Id { get; }
    //    public string Name { get; }
    //    public string? Code { get; }
    //    public string Url { get; }
    //    public string StudyGroupUrl { get; }
    //    public string UniversityUrl { get; }
    //    public string UniversityShortName { get; }
    //    public string StudyGroupLongName { get; }
    //}

    public class GetSearchResultsResponseDto : ResponseBase
    {
        public FoundCourseDto[] RecentCourses { get; set; }
        public FoundCourseDto[] Courses { get; set; }

        public GetSearchResultsResponseDto(FoundCourseDto[] recentCourses, FoundCourseDto[] courses)
        {
            RecentCourses = recentCourses;
            Courses = courses;
        }
    }
}

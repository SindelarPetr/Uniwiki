using System;
using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetSearchResultsResponseDto : ResponseBase
    {
        public RecentCourseDto[] RecentCourses { get; set; }
        public FoundCourseDto[] Courses { get; set; }

        public GetSearchResultsResponseDto(RecentCourseDto[] recentCourses, FoundCourseDto[] courses)
        {
            RecentCourses = recentCourses;
            Courses = courses;
        }
    }
}

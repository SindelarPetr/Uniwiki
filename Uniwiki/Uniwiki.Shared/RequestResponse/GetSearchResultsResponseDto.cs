using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetSearchResultsResponseDto : ResponseBase
    {
        public CourseDto[] RecentCourses { get; set; }
        public CourseDto[] Courses { get; set; }

        public GetSearchResultsResponseDto(CourseDto[] recentCourses, CourseDto[] courses)
        {
            RecentCourses = recentCourses;
            Courses = courses;
        }
    }
}

using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
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

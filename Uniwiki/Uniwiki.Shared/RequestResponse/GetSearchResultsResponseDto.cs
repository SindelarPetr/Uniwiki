using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetSearchResultsResponseDto : ResponseBase
    {
        public UniversityDto[] Universities { get; set; }
        public CourseDto[] RecentCourses { get; set; }
        public StudyGroupDto[] StudyGroups { get; set; }
        public CourseDto[] Courses { get; set; }

        public GetSearchResultsResponseDto(CourseDto[] recentCourses, UniversityDto[] universities, StudyGroupDto[] studyGroups, CourseDto[] courses)
        {
            RecentCourses = recentCourses;
            StudyGroups = studyGroups;
            Courses = courses;
            Universities = universities;
        }
    }
}

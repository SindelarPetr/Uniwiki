using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetCourseRequestDto : RequestBase<GetCourseResponseDto>
    {
        public string UniversityUrl { get; set; }
        public string StudyGroupUrl { get; set; }
        public string CourseUrl { get; set; }
        public string? PostType { get; set; }
        public bool ShowAll { get; set; }
        public int PostsToFetch { get; set; }

        public GetCourseRequestDto(string universityUrl, string studyGroupUrl, string courseUrl, string? postType, bool showAll, int postsToFetch)
        {
            UniversityUrl = universityUrl;
            StudyGroupUrl = studyGroupUrl;
            CourseUrl = courseUrl;
            PostType = postType;
            ShowAll = showAll;
            PostsToFetch = postsToFetch;
        }
    }
}
using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetCourseRequestDto : RequestBase<GetCourseResponseDto>
    {
        public string UniversityUrl { get; }
        public string StudyGroupUrl { get; }
        public string CourseUrl { get; }
        public string? PostType { get;  }
        public bool ShowAll { get;  }
        public int PostsToFetch { get; }
        public int? LastPostNumber { get; }

        public GetCourseRequestDto(string universityUrl, string studyGroupUrl, string courseUrl, string? postType, bool showAll, int postsToFetch, int? lastPostNumber)
        {
            UniversityUrl = universityUrl;
            StudyGroupUrl = studyGroupUrl;
            CourseUrl = courseUrl;
            PostType = postType;
            ShowAll = showAll;
            PostsToFetch = postsToFetch;
            LastPostNumber = lastPostNumber;
        }
    }
}
using System;
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
        public DateTime? LastPostCreationTime { get; }

        public GetCourseRequestDto(string universityUrl, string studyGroupUrl, string courseUrl, string? postType, bool showAll, int postsToFetch, DateTime? lastPostCreationTime)
        {
            UniversityUrl = universityUrl;
            StudyGroupUrl = studyGroupUrl;
            CourseUrl = courseUrl;
            PostType = postType;
            ShowAll = showAll;
            PostsToFetch = postsToFetch;
            LastPostCreationTime = lastPostCreationTime;
        }
    }
}
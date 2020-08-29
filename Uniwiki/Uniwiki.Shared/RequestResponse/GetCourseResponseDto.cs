using System;
using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetCourseResponseDto : ResponseBase
    {
        public string? FocusedPostType { get; set; }
        public PostViewModel[] Posts { get; set; }
        public FilterPostTypeDto[] FilterPostTypes { get; set; }
        public Guid CourseId { get; set; }
        public string?[] NewPostPostTypes { get; set; }
        public bool CanFetchMore { get; set; }
        public RecentCourseDto RecentCourse { get; }
        public string CourseLongName { get; }
        public string CourseUniversityAndFaculty { get; }

        public GetCourseResponseDto(string? focusedPostType, PostViewModel[] posts, FilterPostTypeDto[] filterPostTypes, Guid courseId, string?[] newPostPostTypes, bool canFetchMore, RecentCourseDto recentCourse, string courseLongName, string courseUniversityAndFaculty)
        {
            FocusedPostType = focusedPostType;
            Posts = posts;
            FilterPostTypes = filterPostTypes;
            CourseId = courseId;
            NewPostPostTypes = newPostPostTypes;
            CanFetchMore = canFetchMore;
            RecentCourse = recentCourse;
            CourseLongName = courseLongName;
            CourseUniversityAndFaculty = courseUniversityAndFaculty;
        }
    }
}

using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetCourseResponseDto : ResponseBase
    {
        public string? FocusedPostType { get; set; }
        public PostDto[] Posts { get; set; }
        public FilterPostTypeDto[] FilterPostTypes { get; set; }
        public CourseDto Course { get; set; }
        public string?[] NewPostPostTypes { get; set; }
        public bool CanFetchMore { get; set; }

        public GetCourseResponseDto(string? focusedPostType, PostDto[] posts, FilterPostTypeDto[] filterPostTypes, CourseDto course, string?[] newPostPostTypes, bool canFetchMore)
        {
            FocusedPostType = focusedPostType;
            Posts = posts;
            FilterPostTypes = filterPostTypes;
            Course = course;
            NewPostPostTypes = newPostPostTypes;
            CanFetchMore = canFetchMore;
        }
    }
}

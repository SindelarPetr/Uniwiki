using System;
using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class FetchPostsRequestDto : RequestBase<FetchPostsResponseDto>
    {
        public Guid CourseId { get; set; }
        public Guid LastPostId { get; set; }
        public int PostsToFetch { get; set; }
        public string? PostType { get; set; }
        public bool UsePostTypeFilter { get; set; }

        public FetchPostsRequestDto(Guid courseId, Guid lastPostId, int postsToFetch, bool usePostTypeFilter, string? postType)
        {
            CourseId = courseId;
            LastPostId = lastPostId;
            PostsToFetch = postsToFetch;
            UsePostTypeFilter = usePostTypeFilter;
            PostType = postType;
        }
    }
}
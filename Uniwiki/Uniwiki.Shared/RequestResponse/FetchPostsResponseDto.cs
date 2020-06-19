using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class FetchPostsResponseDto : ResponseBase
    {
        public PostDto[] Posts { get; set; }
        public bool CanFetchMore { get; set; }

        public FetchPostsResponseDto(PostDto[] posts, bool canFetchMore)
        {
            Posts = posts;
            CanFetchMore = canFetchMore;
        }
    }
}
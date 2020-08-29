using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class FetchPostsResponseDto : ResponseBase
    {
        public PostViewModel[] Posts { get; set; }
        public bool CanFetchMore { get; set; }

        public FetchPostsResponseDto(PostViewModel[] posts, bool canFetchMore)
        {
            Posts = posts;
            CanFetchMore = canFetchMore;
        }
    }
}
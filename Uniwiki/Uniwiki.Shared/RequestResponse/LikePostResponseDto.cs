using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class LikePostResponseDto : ResponseBase
    {
        public PostDto Post { get; set; }

        public LikePostResponseDto(PostDto post)
        {
            Post = post;
        }
    }
}
using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class LikePostResponseDto : ResponseBase
    {
        public PostViewModel Post { get; set; }

        public LikePostResponseDto(PostViewModel post)
        {
            Post = post;
        }
    }
}
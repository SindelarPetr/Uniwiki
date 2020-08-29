using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class UnlikePostResponseDto : ResponseBase
    {
        public PostViewModel Post { get; set; }

        public UnlikePostResponseDto(PostViewModel post)
        {
            Post = post;
        }
    }
}
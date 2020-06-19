using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class UnlikePostResponseDto : ResponseBase
    {
        public PostDto Post { get; set; }

        public UnlikePostResponseDto(PostDto post)
        {
            Post = post;
        }
    }
}
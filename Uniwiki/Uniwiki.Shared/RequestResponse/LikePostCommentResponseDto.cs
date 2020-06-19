using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class LikePostCommentResponseDto : ResponseBase
    {
        public PostDto Post { get; set; }

        public LikePostCommentResponseDto(PostDto post)
        {
            Post = post;
        }
    }
}
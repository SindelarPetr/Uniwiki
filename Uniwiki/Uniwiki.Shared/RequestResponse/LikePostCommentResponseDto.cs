using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class LikePostCommentResponseDto : ResponseBase
    {
        public PostViewModel Post { get; set; }

        public LikePostCommentResponseDto(PostViewModel post)
        {
            Post = post;
        }
    }
}
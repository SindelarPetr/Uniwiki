using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class UnlikePostCommentResponseDto : ResponseBase
    {
        public PostViewModel Post { get; set; }

        public UnlikePostCommentResponseDto(PostViewModel post)
        {
            Post = post;
        }
    }
}
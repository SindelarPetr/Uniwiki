using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class UnlikePostCommentResponseDto : ResponseBase
    {
        public PostDto Post { get; set; }

        public UnlikePostCommentResponseDto(PostDto post)
        {
            Post = post;
        }
    }
}
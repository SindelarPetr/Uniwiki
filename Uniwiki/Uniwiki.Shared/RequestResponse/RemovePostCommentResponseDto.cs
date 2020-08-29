using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class RemovePostCommentResponseDto : ResponseBase
    {
        public PostViewModel Post { get; set; }

        public RemovePostCommentResponseDto(PostViewModel post)
        {
            Post = post;
        }
    }
}
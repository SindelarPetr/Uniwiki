using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class RemovePostCommentResponseDto : ResponseBase
    {
        public PostDto Post { get; set; }

        public RemovePostCommentResponseDto(PostDto post)
        {
            Post = post;
        }
    }
}
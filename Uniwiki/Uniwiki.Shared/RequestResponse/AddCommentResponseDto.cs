using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class AddCommentResponseDto : ResponseBase
    {
        public PostViewModel Post { get; set; }

        public AddCommentResponseDto(PostViewModel post)
        {
            Post = post;
        }
    }
}
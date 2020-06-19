using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class AddCommentResponseDto : ResponseBase
    {
        public PostDto Post { get; set; }

        public AddCommentResponseDto(PostDto post)
        {
            Post = post;
        }
    }
}
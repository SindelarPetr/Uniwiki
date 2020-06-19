using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class EditPostResponseDto : ResponseBase
    {
        public PostDto EdittedPost { get; set; }

        public EditPostResponseDto(PostDto edittedPost)
        {
            EdittedPost = edittedPost;
        }
    }
}

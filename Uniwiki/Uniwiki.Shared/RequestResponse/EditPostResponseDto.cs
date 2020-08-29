using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class EditPostResponseDto : ResponseBase
    {
        public PostViewModel EdittedPost { get; set; }

        public EditPostResponseDto(PostViewModel edittedPost)
        {
            EdittedPost = edittedPost;
        }
    }
}

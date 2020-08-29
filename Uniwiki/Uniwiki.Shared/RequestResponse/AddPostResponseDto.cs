using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class AddPostResponseDto : ResponseBase
    {
        public PostViewModel NewPost { get; set; }

        public AddPostResponseDto(PostViewModel newPost)
        {
            NewPost = newPost;
        }
    }
}
using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class AddPostResponseDto : ResponseBase
    {
        public PostDto NewPost { get; set; }

        public AddPostResponseDto(PostDto newPost)
        {
            NewPost = newPost;
        }
    }
}
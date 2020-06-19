using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class EditCommentResponseDto : ResponseBase
    {
        public PostCommentDto PostComment { get; set; }

        public EditCommentResponseDto(PostCommentDto postComment)
        {
            PostComment = postComment;
        }
    }
}
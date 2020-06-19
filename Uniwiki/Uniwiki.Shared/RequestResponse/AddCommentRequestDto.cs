using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class AddCommentRequestDto : RequestBase<AddCommentResponseDto>
    {
        public Guid PostId { get; set; }
        public string CommentText { get; set; }

        public AddCommentRequestDto(Guid postId, string commentText)
        {
            PostId = postId;
            CommentText = commentText;
        }
    }
}
using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class EditCommentRequestDto : RequestBase<EditCommentResponseDto>
    {
        public Guid PostCommentId { get; set; }
        public string Text { get; set; }

        public EditCommentRequestDto(Guid postCommentId, string text)
        {
            PostCommentId = postCommentId;
            Text = text;
        }
    }
}
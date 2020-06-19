using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class UnlikePostCommentRequestDto : RequestBase<UnlikePostCommentResponseDto>
    {
        public Guid PostCommentId { get; set; }

        public UnlikePostCommentRequestDto(Guid postCommentId)
        {
            PostCommentId = postCommentId;
        }
    }
}
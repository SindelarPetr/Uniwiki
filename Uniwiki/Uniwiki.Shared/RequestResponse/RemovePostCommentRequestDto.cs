using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class RemovePostCommentRequestDto : RequestBase<RemovePostCommentResponseDto>
    {
        public Guid PostCommentId { get; set; }

        public RemovePostCommentRequestDto(Guid postCommentId)
        {
            PostCommentId = postCommentId;
        }
    }
}
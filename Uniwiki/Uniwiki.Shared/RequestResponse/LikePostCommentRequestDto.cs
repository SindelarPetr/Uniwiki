using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class LikePostCommentRequestDto : RequestBase<LikePostCommentResponseDto>
    {
        public Guid PostCommentId { get; }


        public LikePostCommentRequestDto(Guid postCommentId)
        {
            PostCommentId = postCommentId;
        }
    }
}
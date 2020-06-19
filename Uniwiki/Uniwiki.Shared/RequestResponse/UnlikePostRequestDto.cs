using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class UnlikePostRequestDto : RequestBase<UnlikePostResponseDto>
    {
        public Guid PostId { get; set; }

        public UnlikePostRequestDto(Guid postId)
        {
            PostId = postId;
        }
    }
}
using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class LikePostRequestDto : RequestBase<LikePostResponseDto>
    {
        public LikePostRequestDto(Guid postId)
        {
            PostId = postId;
        }

        public Guid PostId { get; set; }
    }
}
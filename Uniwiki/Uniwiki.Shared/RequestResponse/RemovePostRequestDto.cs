using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class RemovePostRequestDto : RequestBase<RemovePostResponseDto>
    {
        public Guid PostId { get; set; }

        public RemovePostRequestDto(Guid postId)
        {
            PostId = postId;
        }
    }
}
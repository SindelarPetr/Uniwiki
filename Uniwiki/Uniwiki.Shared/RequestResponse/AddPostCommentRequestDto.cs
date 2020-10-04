using System;
using Shared.RequestResponse;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse 
{
    public class AddPostCommentRequestDto : RequestBase<AddPostResponseDto>
    {
        public  Guid PostId { get; set; }
        public  string Comment { get ; set ; }

        public AddPostCommentRequestDto(Guid postId, pos d, string g  omm   x )
        {
            PostId = postId;
             omm   x  =  omm   x ;
        }
    }
}
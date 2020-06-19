using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class UploadProfileImageResponseDto : ResponseBase
    {
        public Guid FileId { get; set; }

        public UploadProfileImageResponseDto(Guid fileId)
        {
            FileId = fileId;
        }
    }
}
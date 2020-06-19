using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class UploadProfileImageRequestDto : RequestBase<UploadProfileImageResponseDto>
    {
        public Guid Id { get; set; }
        public string OriginalName { get; set; }

        public UploadProfileImageRequestDto(Guid id, string originalName)
        {
            Id = id;
            OriginalName = originalName;
        }
    }
}
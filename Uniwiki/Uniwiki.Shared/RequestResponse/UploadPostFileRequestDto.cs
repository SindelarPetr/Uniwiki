using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class UploadPostFileRequestDto : RequestBase<UploadPostFileResponseDto> 
    {
        public string OriginalName { get; set; }
        public Guid CourseId { get; set; }

        public UploadPostFileRequestDto(string originalName, Guid courseId)
        {
            OriginalName = originalName;
            CourseId = courseId;
        }
    }
}
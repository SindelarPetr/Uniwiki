using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class UploadPostFileResponseDto : ResponseBase
    {
        public PostFileDto PostFile { get; set; }

        public UploadPostFileResponseDto(PostFileDto postFile)
        {
            PostFile = postFile;
        }
    }
}
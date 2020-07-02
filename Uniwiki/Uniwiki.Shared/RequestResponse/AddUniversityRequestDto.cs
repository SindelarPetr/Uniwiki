using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class AddUniversityRequestDto : RequestBase<AddUniversityResponseDto>
    {
        public AddUniversityRequestDto(string fullName, string shortName, string url)
        {
            FullName = fullName;
            ShortName = shortName;
            Url = url;
        }

        public string FullName { get; }
        public string ShortName { get; }
        public string Url { get; }
    }
}

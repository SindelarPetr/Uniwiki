using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetProfileRequest : RequestBase<GetProfileResponse>
    {
        public string Url { get; set; }

        public GetProfileRequest(string url)
        {
            Url = url;
        }
    }
}

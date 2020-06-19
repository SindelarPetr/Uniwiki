using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetProfileRequest : RequestBase<GetProfileResponse>
    {
        public string NameIdentifier { get; set; }

        public GetProfileRequest(string nameIdentifier)
        {
            NameIdentifier = nameIdentifier;
        }
    }
}

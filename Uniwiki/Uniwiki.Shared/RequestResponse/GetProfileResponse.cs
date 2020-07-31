using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetProfileResponse : ResponseBase
    {
        public ProfileDto Profile { get; set; }
        public bool Authenticated { get; }

        public GetProfileResponse(ProfileDto profile, bool authenticated)
        {
            Profile = profile;
            Authenticated = authenticated;
        }
    }
}
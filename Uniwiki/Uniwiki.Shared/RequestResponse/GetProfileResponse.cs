using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetProfileResponse : ResponseBase
    {
        public ProfileViewModel Profile { get; set; }
        public bool Authenticated { get; }

        public GetProfileResponse(ProfileViewModel profile, bool authenticated)
        {
            Profile = profile;
            Authenticated = authenticated;
        }
    }
}
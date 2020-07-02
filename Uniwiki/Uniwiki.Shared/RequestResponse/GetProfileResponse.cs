using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetProfileResponse : ResponseBase
    {
        public ProfileDto Profile { get; }

        public GetProfileResponse(ProfileDto profile)
        {
            Profile = profile;
        }
    }
}
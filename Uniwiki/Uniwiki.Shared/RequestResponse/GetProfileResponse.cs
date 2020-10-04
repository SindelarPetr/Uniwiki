using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Shared.RequestResponse
{
    public class GetProfileResponse : ResponseBase
    {
        public ProfileViewModel Profile { get; set; }
        public bool IsAuthenticated => AuthorizedUser != null;
        public AuthorizedUserDto? AuthorizedUser { get; }

        public GetProfileResponse(ProfileViewModel profile, AuthorizedUserDto? authorizedUser)
        {
            Profile = profile;
            AuthorizedUser = authorizedUser;
        }
    }
}
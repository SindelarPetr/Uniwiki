using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Shared.RequestResponse
{
    public class EditHomeFacultyResponseDto : ResponseBase
    {
        public EditHomeFacultyResponseDto(ProfileViewModel profile, AuthorizedUserDto? authorizedUser)
        {
            Profile = profile;
            AuthorizedUser = authorizedUser;
        }

        public ProfileViewModel Profile { get; }
        public AuthorizedUserDto? AuthorizedUser { get; }
    }
}

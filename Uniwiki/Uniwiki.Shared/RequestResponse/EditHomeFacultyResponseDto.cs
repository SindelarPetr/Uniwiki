using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Shared.RequestResponse
{
    public class EditHomeFacultyResponseDto : ResponseBase
    {
        public EditHomeFacultyResponseDto(AuthorizedUserDto profile)
        {
            Profile = profile;
        }

        public AuthorizedUserDto Profile { get; }
    }
}

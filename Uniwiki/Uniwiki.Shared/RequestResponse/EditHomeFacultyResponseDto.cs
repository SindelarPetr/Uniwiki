using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class EditHomeFacultyResponseDto : ResponseBase
    {
        public EditHomeFacultyResponseDto(ProfileDto profile)
        {
            Profile = profile;
        }

        public ProfileDto Profile { get; }
    }
}

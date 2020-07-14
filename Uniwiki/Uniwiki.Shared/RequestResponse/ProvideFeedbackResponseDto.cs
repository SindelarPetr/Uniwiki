using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class ProvideFeedbackResponseDto : ResponseBase
    {

        public ProfileDto? User { get; }

        public ProvideFeedbackResponseDto(ProfileDto? user)
        {
            User = user;
        }
    }
}

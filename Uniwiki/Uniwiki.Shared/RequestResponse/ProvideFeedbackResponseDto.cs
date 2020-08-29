using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Shared.RequestResponse
{
    public class ProvideFeedbackResponseDto : ResponseBase
    {
        public AuthorizedUserDto? User { get; }

        public ProvideFeedbackResponseDto(AuthorizedUserDto? user)
        {
            User = user;
        }
    }
}

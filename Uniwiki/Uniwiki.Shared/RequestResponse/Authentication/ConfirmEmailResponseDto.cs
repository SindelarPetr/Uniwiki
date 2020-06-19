using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class ConfirmEmailResponseDto : ResponseBase
    {
        public ProfileDto Profile { get; set; }
        public LoginTokenDto? LoginToken { get; set; }

        public ConfirmEmailResponseDto(ProfileDto profile, LoginTokenDto? loginToken)
        {
            Profile = profile;
            LoginToken = loginToken;
        }
    }
}
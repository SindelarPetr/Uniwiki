using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class LoginResponseDto : ResponseBase
    {
        public LoginTokenDto LoginToken { get; set; }
        public ProfileDto User { get; set; }

        public LoginResponseDto(LoginTokenDto loginToken, ProfileDto user)
        {
            LoginToken = loginToken;
            User = user;
        }
    }
}

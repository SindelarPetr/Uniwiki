using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class LoginResponseDto : ResponseBase
    {
        public LoginTokenDto LoginToken { get; set; }
        public AuthorizedUserDto User { get; set; }

        public LoginResponseDto(LoginTokenDto loginToken, AuthorizedUserDto user)
        {
            LoginToken = loginToken;
            User = user;
        }
    }
}

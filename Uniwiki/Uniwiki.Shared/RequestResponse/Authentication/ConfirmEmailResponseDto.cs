using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class ConfirmEmailResponseDto : ResponseBase
    {
        public AuthorizedUserDto AuthorizedUser { get; }
        public LoginTokenDto? LoginToken { get; }

        public ConfirmEmailResponseDto(AuthorizedUserDto authorizedUser, LoginTokenDto? loginToken)
        {
            AuthorizedUser = authorizedUser;
            LoginToken = loginToken;
        }
    }
}
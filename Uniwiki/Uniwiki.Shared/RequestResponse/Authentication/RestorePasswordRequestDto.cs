using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class RestorePasswordRequestDto : RequestBase<RestorePasswordResponseDto>
    {
        public string Email { get; set; }

        public RestorePasswordRequestDto(string email)
        {
            Email = email;
        }
    }
}
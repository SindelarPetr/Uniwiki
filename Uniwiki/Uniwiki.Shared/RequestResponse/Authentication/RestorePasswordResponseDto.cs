using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class RestorePasswordResponseDto : ResponseBase
    {
        public string Email { get; set; }

        public RestorePasswordResponseDto(string email)
        {
            Email = email;
        }
    }
}
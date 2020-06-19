using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class ResendConfirmationEmailRequestDto : RequestBase<ResendConfirmationEmailResponseDto>
    {
        public string Email { get; set; }

        public ResendConfirmationEmailRequestDto(string email)
        {
            Email = email;
        }
    }
}
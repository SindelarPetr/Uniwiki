using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class IsEmailAvailableRequestDto : RequestBase<IsEmailAvailableResponseDto>
    {
        public string Email { get; set; }

        public IsEmailAvailableRequestDto(string email)
        {
            Email = email;
        }

    }
}

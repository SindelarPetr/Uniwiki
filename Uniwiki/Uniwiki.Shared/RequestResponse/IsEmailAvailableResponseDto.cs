using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class IsEmailAvailableResponseDto : ResponseBase
    {
        public bool IsAvailable { get; set; }

        public IsEmailAvailableResponseDto(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }
    }
}

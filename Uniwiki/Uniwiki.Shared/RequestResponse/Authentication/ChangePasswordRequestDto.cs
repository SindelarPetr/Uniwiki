using Shared.RequestResponse;
using Uniwiki.Shared.Attributes;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class ChangePasswordRequestDto : RequestBase<ChangePasswordResponseDto>
    {
        [DontLog]
        public string OldPassword { get; set; }
        [DontLog]
        public string NewPassword { get; set; }
        [DontLog]
        public string NewPasswordAgain { get; set; }

        public ChangePasswordRequestDto(string oldPassword, string newPassword, string newPasswordAgain)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
            NewPasswordAgain = newPasswordAgain;
        }
    }
}
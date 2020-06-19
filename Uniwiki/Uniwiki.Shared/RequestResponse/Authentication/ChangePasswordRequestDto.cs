using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class ChangePasswordRequestDto : RequestBase<ChangePasswordResponseDto>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordAgain { get; set; }

        public ChangePasswordRequestDto(string oldPassword, string newPassword, string newPasswordAgain)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
            NewPasswordAgain = newPasswordAgain;
        }
    }
}
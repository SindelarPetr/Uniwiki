using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class CreateNewPasswordRequestDto : RequestBase<CreateNewPasswordResponseDto>
    {
        public string NewPassword { get; set; }
        public Guid Secret { get; set; }
        public string NewPasswordAgain { get; set; }

        public CreateNewPasswordRequestDto(string newPassword, Guid secret, string newPasswordAgain)
        {
            NewPassword = newPassword;
            Secret = secret;
            NewPasswordAgain = newPasswordAgain;
        }

    }
}
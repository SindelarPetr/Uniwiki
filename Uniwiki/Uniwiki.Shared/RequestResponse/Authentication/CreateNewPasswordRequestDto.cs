using System;
using Shared.RequestResponse;
using Uniwiki.Shared.Attributes;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class CreateNewPasswordRequestDto : RequestBase<CreateNewPasswordResponseDto>
    {
        [DontLog]
        public string NewPassword { get; set; }
        public Guid Secret { get; set; }
        [DontLog]
        public string NewPasswordAgain { get; set; }

        public CreateNewPasswordRequestDto(string newPassword, Guid secret, string newPasswordAgain)
        {
            NewPassword = newPassword;
            Secret = secret;
            NewPasswordAgain = newPasswordAgain;
        }

    }
}
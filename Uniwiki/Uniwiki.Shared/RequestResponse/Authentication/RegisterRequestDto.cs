using Shared.RequestResponse;
using Uniwiki.Shared.Attributes;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class RegisterRequestDto : RequestBase<RegisterResponseDto>
    {
        public string Email { get; set; }
        [DontLog]
        public string Password { get; set; }
        public string NameAndSurname { get; set; }
        [DontLog]
        public string PasswordAgain { get; set; }

        public bool AgreeToTermsOfUse { get; set; }

        public RegisterRequestDto(string email, string nameAndSurname, string password, string passwordAgain, bool agreeToTermsOfUse)
        {
            Email = email;
            NameAndSurname = nameAndSurname;
            Password = password;
            PasswordAgain = passwordAgain;
            AgreeToTermsOfUse = agreeToTermsOfUse;
        }
    }
}

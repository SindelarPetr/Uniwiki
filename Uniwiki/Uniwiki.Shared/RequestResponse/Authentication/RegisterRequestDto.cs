using Shared.RequestResponse;
using Uniwiki.Shared.Attributes;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class RegisterRequestDto : RequestBase<RegisterResponseDto>
    {
        public string Email { get; set; }
        [DontLog]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [DontLog]
        public string PasswordAgain { get; set; }

        public bool AgreeToTermsOfUse { get; set; }

        public RegisterRequestDto(string email, string name, string surname, string password, string passwordAgain, bool agreeToTermsOfUse)
        {
            Email = email;
            Name = name;
            Surname = surname;
            Password = password;
            PasswordAgain = passwordAgain;
            AgreeToTermsOfUse = agreeToTermsOfUse;
        }
    }
}

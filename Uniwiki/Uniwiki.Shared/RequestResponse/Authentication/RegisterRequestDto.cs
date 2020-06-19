using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class RegisterRequestDto : RequestBase<RegisterResponseDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PasswordAgain { get; set; }

        public RegisterRequestDto(string email, string name, string surname, string password, string passwordAgain)
        {
            Email = email;
            Name = name;
            Surname = surname;
            Password = password;
            PasswordAgain = passwordAgain;
        }
    }
}

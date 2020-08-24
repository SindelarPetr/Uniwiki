using Shared.RequestResponse;
using Uniwiki.Shared.Attributes;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class LoginRequestDto : RequestBase<LoginResponseDto>
    {
        public string Email { get; set; }
        [DontLog]
        public string Password { get; set; }
        public FoundCourseDto[] RecentCourses { get; set; }

        public LoginRequestDto(string email, string password, FoundCourseDto[] recentCourses)
        {
            Email = email;
            Password = password;
            RecentCourses = recentCourses;
        }
    }
}

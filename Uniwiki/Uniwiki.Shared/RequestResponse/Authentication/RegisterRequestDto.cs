using Shared.RequestResponse;
using System;
using Uniwiki.Shared.Attributes;
using Uniwiki.Shared.ModelDtos;

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

        public Guid? HomeFacultyId { get; set; }
        public CourseDto[] RecentCourses { get; }

        public RegisterRequestDto(string email, string nameAndSurname, string password, string passwordAgain, bool agreeToTermsOfUse, Guid? homeFacultyId, CourseDto[] recentCourses)
        {
            Email = email;
            NameAndSurname = nameAndSurname;
            Password = password;
            PasswordAgain = passwordAgain;
            AgreeToTermsOfUse = agreeToTermsOfUse;
            HomeFacultyId = homeFacultyId;
            RecentCourses = recentCourses;
        }
    }
}

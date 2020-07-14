using System.Collections.Generic;
using System.Threading.Tasks;
using Shared;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Client.Host.Services.Abstractions
{
    public interface ILocalStorageManagerService
    {
        Task<Language?> GetCurrentLanguage();
        Task SetCurrentLanguage(Language language);

        Task<LoginTokenDto?> GetLoginToken();
        Task SetLoginToken(LoginTokenDto loginToken);
        Task RemoveLoginToken();

        Task<ProfileDto?> GetLoginProfile();
        Task SetLoginProfile(ProfileDto loginProfile);
        Task RemoveLoginProfile();
        Task SetRecentCourses(CourseDto[] courses);
        Task SetRecentCourse(CourseDto course);
        Task<CourseDto[]> GetRecentCourses();

        Task<bool> IsFeedbackProvided();
        Task SetFeedbackProvided();
    }
}

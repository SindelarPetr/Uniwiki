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

        Task<AuthorizedUserDto?> GetLoginProfile();
        Task SetLoginProfile(AuthorizedUserDto loginProfile);
        Task RemoveLoginProfile();
        Task SetRecentCourses(RecentCourseDto[] courses);
        Task SetRecentCourse(RecentCourseDto course);
        Task<RecentCourseDto[]> GetRecentCourses();

        Task<bool> IsFeedbackProvided();
        Task SetFeedbackProvided();
    }
}

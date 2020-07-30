using Uniwiki.Server.Persistence.Models;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Server.Application.Services.Abstractions
{
    public interface IRecentCoursesService
    {
        void SetAsRecentCourses(CourseDto[] recentCoursesDtos, ProfileModel profile);
    }
}
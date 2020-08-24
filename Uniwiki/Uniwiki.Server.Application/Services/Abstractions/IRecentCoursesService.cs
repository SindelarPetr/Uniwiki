using System;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Server.Application.Services.Abstractions
{
    public interface IRecentCoursesService
    {
        void SetAsRecentCourses(FoundCourseDto[] recentCoursesDtos, Guid profileId);
    }
}
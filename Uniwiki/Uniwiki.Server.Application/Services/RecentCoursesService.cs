using Shared.Services.Abstractions;
using System;
using System.Linq;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Server.Application.Services
{
    public class RecentCoursesService : IRecentCoursesService
    {
        private readonly ITimeService _timeService;
        private readonly UniwikiContext _uniwikiContext;

        public RecentCoursesService(ITimeService timeService, UniwikiContext uniwikiContext)
        {
            _timeService = timeService;
            _uniwikiContext = uniwikiContext;
        }

        public void SetAsRecentCourses(FoundCourseDto[] recentCoursesDtos, Guid profileId)
        {
            // Set the recent courses
            var courseVisits = recentCoursesDtos.Select(c => new CourseVisitModel(Guid.NewGuid(), c.Id, profileId, _timeService.Now));

            // Add it to the DB
            _uniwikiContext.CourseVisits.AddRange(courseVisits);
        }
    }
}

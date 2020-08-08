using Shared.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Server.Application.Services
{
    public class RecentCoursesService : IRecentCoursesService
    {
        private readonly ITimeService _timeService;
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseVisitRepository _courseVisitRepository;

        public RecentCoursesService(ITimeService timeService, ICourseRepository courseRepository, ICourseVisitRepository courseVisitRepository)
        {
            _timeService = timeService;
            _courseRepository = courseRepository;
            _courseVisitRepository = courseVisitRepository;
        }

        public void SetAsRecentCourses(CourseDto[] recentCoursesDtos, ProfileModel profile)
        {
            // Get recent courses
            var recentCourses = _courseRepository.TryGetCourses(recentCoursesDtos.Select(c => (c.Url, c.StudyGroup.Url, c.University.Url)));

            // Set the recent courses
            _courseVisitRepository.AddRecentCourseVisits(recentCourses, profile, _timeService.Now);
        }
    }
}

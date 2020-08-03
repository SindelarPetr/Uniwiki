using System;
using System.Collections.Generic;
using System.Linq;
using Uniwiki.Server.Persistence.InMemory.Services;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared;

namespace Uniwiki.Server.Persistence.InMemory.Repositories
{
    internal class CourseVisitInMemoryRepository : ICourseVisitRepository
    {
        private readonly DataService _dataService;

        public CourseVisitInMemoryRepository(DataService dataService)
        {
            _dataService = dataService;
        }

        public IEnumerable<CourseModel> GetRecentCourses(StudyGroupModel? studyGroup, ProfileModel profile)
        {
            return profile.RecentCourses
                //.Where(c => c.StudyGroup == studyGroup || studyGroup == null)
                .Reverse()
                .Take(Constants.NumberOrRecentCourses)
                .Reverse();
        }

        public void AddCourseVisit(CourseModel course, ProfileModel profile, DateTime visitTime)
        {
            var newCourseVisit = new CourseVisitModel(course, profile, visitTime);

            _dataService.CourseVisits.Add(newCourseVisit);
        }

        public void AddRecentCourseVisits(IEnumerable<CourseModel> recentCourses, ProfileModel profile, DateTime visitTime)
        {
            foreach (var course in recentCourses.Reverse())
            {
                AddCourseVisit(course, profile, visitTime);
            }
        }
    }
}
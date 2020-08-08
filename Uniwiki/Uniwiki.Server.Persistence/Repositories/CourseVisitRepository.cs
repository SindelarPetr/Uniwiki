using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;
using Uniwiki.Shared;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class CourseVisitRepository : ICourseVisitRepository
    {
        public DbSet<CourseVisitModel> All { get; }

        public CourseVisitRepository(UniwikiContext uniwikiContext)
        {
            All = uniwikiContext.CourseVisits;
        }

        public IEnumerable<CourseModel> GetRecentCourses(StudyGroupModel? studyGroup, ProfileModel profile)
        {
            return profile.RecentCourses
                //.Where(c => c.StudyGroup == studyGroup || studyGroup == null)
                .Reverse()
                .Take(Constants.NumberOrRecentCourses)
                .Reverse();
        }

        public void AddRecentCourseVisits(IEnumerable<CourseModel> recentCourses, ProfileModel profile, DateTime visitTime)
        {
            foreach (var course in recentCourses.Reverse())
            {
                var courseVisit = new CourseVisitModel(Guid.NewGuid(), course, profile, visitTime);
                (this as IRepository<CourseVisitModel>).Add(courseVisit);
            }
        }
    }
}
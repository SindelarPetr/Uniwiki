using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.Services;
using Uniwiki.Shared;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class CourseVisitRepository : RepositoryBase<CourseVisitModel, Guid>, ICourseVisitRepository
    {
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.Error_CourseVisitNotFound;

        public CourseVisitRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.CourseVisits)
        {
            _textService = textService;
        }

        public IEnumerable<CourseModel> GetRecentCourses(StudyGroupModel? studyGroup, ProfileModel profile)
        {
            return profile.RecentCourses
                .Reverse()
                .Take(Constants.NumberOrRecentCourses)
                .Reverse();
        }

        public void AddRecentCourseVisits(IEnumerable<CourseModel> recentCourses, ProfileModel profile, DateTime visitTime)
        {
            foreach (var course in recentCourses.Reverse())
            {
                AddCourseVisit(course, profile, visitTime);
            }
        }

        public CourseVisitModel AddCourseVisit(CourseModel course, ProfileModel profile, DateTime visitTime)
        {
            var courseVisit = new CourseVisitModel(Guid.NewGuid(), course, profile, visitTime);

            All.Add(courseVisit);

            return courseVisit;
        }
    }
}
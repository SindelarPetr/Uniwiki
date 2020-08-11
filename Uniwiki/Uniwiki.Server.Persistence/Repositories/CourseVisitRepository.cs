using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;
using Uniwiki.Server.Persistence.Services;
using Uniwiki.Shared;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class CourseVisitRepository : RepositoryBase<CourseVisitModel>, ICourseVisitRepository
    {
        private readonly TextService _textService;

        public string NotFoundByIdMessage => _textService.Error_CourseVisitNotFound;

        public CourseVisitRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.CourseVisits)
        {
            _textService = textService;
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
                Add(courseVisit);
            }
        }
    }
}
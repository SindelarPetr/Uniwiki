using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface ICourseVisitRepository : IRepositoryBase<CourseVisitModel, Guid>
    {
        IEnumerable<CourseModel> GetRecentCourses(StudyGroupModel? studyGroup, ProfileModel profile);
        void AddRecentCourseVisits(IEnumerable<CourseModel> recentCourses, ProfileModel profile, DateTime visitTime);
        CourseVisitModel AddCourseVisit(CourseModel course, ProfileModel profile, DateTime visitTime);
    }
}

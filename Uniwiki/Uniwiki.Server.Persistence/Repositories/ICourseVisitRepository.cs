using System;
using System.Collections.Generic;
using System.Text;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence.Repositories
{
    public interface ICourseVisitRepository
    {
        IEnumerable<CourseModel> GetRecentCourses(StudyGroupModel? studyGroup, ProfileModel profile);
        void AddCourseVisit(CourseModel course, ProfileModel profile, DateTime visitTime);
        void AddRecentCourseVisits(IEnumerable<CourseModel> recentCourses, ProfileModel profile, DateTime visitTime);
    }
}

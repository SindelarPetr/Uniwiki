using System;

namespace Uniwiki.Server.Persistence.Models
{
    public class CourseVisitModel
    {
        public CourseModel Course { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public DateTime VisitDateTime { get; protected set; }

        internal CourseVisitModel()
        {

        }

        internal CourseVisitModel(CourseModel course, ProfileModel profile, DateTime visitDateTime)
        {
            Course = course;
            Profile = profile;
            VisitDateTime = visitDateTime;
        }
    }
}
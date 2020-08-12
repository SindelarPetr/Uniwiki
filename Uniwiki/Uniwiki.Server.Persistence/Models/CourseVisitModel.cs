using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class CourseVisitModel : ModelBase<Guid>
    {
        public CourseModel Course { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public DateTime VisitDateTime { get; protected set; }

        internal CourseVisitModel(Guid id, CourseModel course, ProfileModel profile, DateTime visitDateTime)
            :base(id)
        {
            Course = course;
            Profile = profile;
            VisitDateTime = visitDateTime;
        }

        protected CourseVisitModel()
        {

        }
    }
}
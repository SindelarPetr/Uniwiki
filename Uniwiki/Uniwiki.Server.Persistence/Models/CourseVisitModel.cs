using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class CourseVisitModel : ModelBase<Guid>
    {
        public Guid CourseId { get; protected set; }
        public CourseModel Course { get; protected set; } = null!;
        public Guid ProfileId { get; protected set; }
        public ProfileModel Profile { get; protected set; } = null!;
        public DateTime VisitDateTime { get; protected set; }

        internal CourseVisitModel(Guid id, CourseModel course, ProfileModel profile, DateTime visitDateTime)
            :base(id)
        {
            CourseId = course.Id;
            Course = course;
            ProfileId = profile.Id;
            Profile = profile;
            VisitDateTime = visitDateTime;
        }

        protected CourseVisitModel()
        {

        }
    }
}
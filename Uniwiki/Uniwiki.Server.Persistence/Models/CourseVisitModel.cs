using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class CourseVisitModel : IIdModel<Guid>
    {
        public CourseModel Course { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public DateTime VisitDateTime { get; protected set; }
        public Guid Id { get; protected set; }

        public CourseVisitModel(Guid id, CourseModel course, ProfileModel profile, DateTime visitDateTime)
        {
            Id = id;
            Course = course;
            Profile = profile;
            VisitDateTime = visitDateTime;
        }

        protected CourseVisitModel()
        {

        }
    }
}
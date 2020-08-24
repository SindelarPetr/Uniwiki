using System;
using Toolbelt.ComponentModel.DataAnnotations.Schema.V5;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class CourseVisitModel : ModelBase<Guid>
    {
        public Guid CourseId { get; protected set; }
        public CourseModel Course { get; protected set; } = null!;
        public Guid ProfileId { get; protected set; }
        public ProfileModel Profile { get; protected set; } = null!;
        [IndexColumn]
        public DateTime VisitDateTime { get; protected set; }

        public CourseVisitModel(Guid id, Guid courseId, Guid profileId, DateTime visitDateTime)
            :base(id)
        {
            CourseId = courseId;
            ProfileId = profileId;
            VisitDateTime = visitDateTime;
        }

        protected CourseVisitModel()
        {

        }
    }
}
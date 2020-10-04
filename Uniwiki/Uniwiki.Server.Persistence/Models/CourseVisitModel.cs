using System;
using System.Collections.Generic;
using Toolbelt.ComponentModel.DataAnnotations.Schema.V5;
using Uniwiki.Server.Persistence.Models.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class CourseVisitModel : ModelBase<Guid>
    {
        public Guid CourseId { get; set; }
        public CourseModel Course { get; set; } = null!;
        public Guid ProfileId { get; set; }
        public ProfileModel Profile { get; set; } = null!;
        [IndexColumn]
        public DateTime VisitDateTime { get; set; }

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
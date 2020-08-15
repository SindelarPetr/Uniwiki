using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class CourseVisitModelMap : ModelMapBase<CourseVisitModel, Guid>
    {
        public CourseVisitModelMap() : base("CourseVisits")
        {

        }
    }
}
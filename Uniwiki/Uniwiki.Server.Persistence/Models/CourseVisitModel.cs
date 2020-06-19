using System;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence.InMemory.Services
{
    public class CourseVisitModel
    {
        public CourseModel Course { get; }
        public ProfileModel Profile { get; }
        public DateTime VisitDateTime { get; }

        public CourseVisitModel(CourseModel course, ProfileModel profile, DateTime visitDateTime)
        {
            Course = course;
            Profile = profile;
            VisitDateTime = visitDateTime;
        }
    }
}
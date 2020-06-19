using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence.Repositories
{
    public interface ICourseRepository
    {
        /// <returns>All courses in DB.</returns>
        IEnumerable<CourseModel> GetCourses();

        /// <summary>
        /// Returns all courses which it will be able to find. If a course with some ID does not exist, then it will skip it.
        /// </summary>
        IEnumerable<CourseModel> TryGetCourses(IEnumerable<(string courseUrl, string studyGroupUrl, string universityUrl)> urls); 

        /// <returns>The found course.</returns>
        CourseModel GetCourse(string universityUrl, string studyGroupUrl, string courseUrl);
        CourseModel FindById(Guid id);
        IEnumerable<CourseModel> SearchCourses(string text);
        IEnumerable<CourseModel> SearchCoursesFromStudyGroup(string text, StudyGroupModel studyGroup);
        IEnumerable<CourseModel> SearchCoursesFromUniversity(string text, UniversityModel university);
        CourseModel CreateCourse(string code, string fullname, StudyGroupModel studyGroup, ProfileModel author, string url);
        bool IsUrlUnique(StudyGroupModel studyGroup, string url);
        bool IsNameUnique(StudyGroupModel studyGroup, string name);
    }
}
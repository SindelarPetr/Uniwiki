using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface ICourseRepository : IRemovableRepositoryBase<CourseModel, Guid>
    {
        /// <summary>
        /// Returns all courses which it will be able to find.
        /// </summary>
        IEnumerable<CourseModel> TryGetCourses(IEnumerable<(string courseUrl, string studyGroupUrl, string universityUrl)> urls);

        /// <returns>The found course.</returns>
        CourseModel GetCourseFromUrl(string universityUrl, string studyGroupUrl, string courseUrl);
        IEnumerable<CourseModel> SearchCourses(string text);
        IEnumerable<CourseModel> SearchCoursesFromStudyGroup(string text, StudyGroupModel studyGroup);
        IEnumerable<CourseModel> SearchCoursesFromUniversity(string text, UniversityModel university);
        bool IsUrlUnique(StudyGroupModel studyGroup, string url);
        bool IsNameUnique(StudyGroupModel studyGroup, string name);
        CourseModel GetCourseWithStudyGroupAndUniversity(Guid id);
        CourseModel AddCourse(string code, string fullName, ProfileModel author, StudyGroupModel faculty, string universityUrl, string url);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Shared;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Services;
using Uniwiki.Server.Persistence.InMemory.Services;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Services;
using Uniwiki.Shared;

namespace Uniwiki.Server.Persistence.InMemory.Repositories
{
    internal class CourseInMemoryRepository : ICourseRepository
    {
        private readonly DataService _dataService;
        private readonly IStringStandardizationService _stringStandardizationService;
        private readonly TextService _textService;

        public CourseInMemoryRepository(DataService dataService, IStringStandardizationService stringStandardizationService, TextService textService)
        {
            _dataService = dataService;
            _stringStandardizationService = stringStandardizationService;
            _textService = textService;
        }

        public IEnumerable<CourseModel> GetCourses()
        {
            return _dataService.Courses;
        }

        public CourseModel GetCourse(string universityUrl, string studyGroupUrl, string courseUrl)
        {
            return _dataService.Courses.FirstOrDefault(c =>
                c.StudyGroup.University.Url == universityUrl.Neutralize() &&
                c.StudyGroup.Url == studyGroupUrl.Neutralize() && c.Url == courseUrl.Neutralize()) ?? throw new RequestException(_textService.Error_CourseNotFound);
        }

        public CourseModel FindById(Guid id)
        {
            return _dataService.Courses.FirstOrDefault(c => c.Id == id) ?? throw new RequestException(_textService.Error_CourseNotFound);
        }

        public IEnumerable<CourseModel> SearchCourses(string text)
        {
            return GetCourses().Where(c =>
                _stringStandardizationService.StandardizeSearchText(c.Code).Contains(text) ||
                _stringStandardizationService.StandardizeSearchText(c.FullName).Contains(text));
        }

        public IEnumerable<CourseModel> SearchCoursesFromStudyGroup(string text, StudyGroupModel studyGroup)
        {
            return SearchCourses(text).Where(c => c.StudyGroup == studyGroup);
        }

        public IEnumerable<CourseModel> SearchCoursesFromUniversity(string text, UniversityModel university)
        {
            return SearchCourses(text).Where(c => c.StudyGroup.University == university);
        }

        public CourseModel CreateCourse(string code, string fullname, StudyGroupModel studyGroup, ProfileModel author, string url)
        {
            // Create a new Id for the course
            var id = Guid.NewGuid();

            // Get posts
            var posts = _dataService.Posts.Where(p => p.Course.Id == id);

            // Get post types
            var postTypes = posts.Select(p => p.PostType).Concat(studyGroup.PrimaryLanguage == Language.Czech ? _dataService._defaultPostTypesCz : _dataService._defaultPostTypesEn).Distinct();

            // Create the course
            var course = new CourseModel(id, code, fullname, studyGroup, author, url, posts, postTypes);
            
            // Save the course to DB
            _dataService._courses.Add(course);

            return course;
        }

        public bool IsUrlUnique(StudyGroupModel studyGroup, string url)
        {
            return studyGroup.Courses.All(c => c.Url != url);
        }

        public bool IsNameUnique(StudyGroupModel studyGroup, string name)
        {
            return studyGroup.Courses.All(c => c.FullName.ToLower().Trim() != name.ToLower().Trim());
        }

        public IEnumerable<CourseModel> TryGetCourses(IEnumerable<(string courseUrl, string studyGroupUrl, string universityUrl)> urls)
        {
            return urls.Select(course => _dataService.Courses.FirstOrDefault(c => c.Url == course.courseUrl && c.StudyGroup.Url == course.studyGroupUrl && c.StudyGroup.University.Url == course.universityUrl)).Where(c => c != null);
        }
    }
}

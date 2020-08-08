using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{


    internal class CourseRepository : ICourseRepository
    {
        private readonly UniwikiContext _uniwikiContext;
        private readonly IStringStandardizationService _stringStandardizationService;
        private readonly TextService _textService;

        public DbSet<CourseModel> All => _uniwikiContext.Courses;

        public CourseRepository(UniwikiContext uniwikiContext, IStringStandardizationService stringStandardizationService, TextService textService)
        {
            _uniwikiContext = uniwikiContext;
            _stringStandardizationService = stringStandardizationService;
            _textService = textService;
        }

        public CourseModel GetCourse(string universityUrl, string studyGroupUrl, string courseUrl)
        {
            return _uniwikiContext.Courses.FirstOrDefault(c =>
                c.StudyGroup.University.Url == universityUrl.Neutralize() &&
                c.StudyGroup.Url == studyGroupUrl.Neutralize() && c.Url == courseUrl.Neutralize()) ?? throw new RequestException(_textService.Error_CourseNotFound);
        }

        public IEnumerable<CourseModel> SearchCourses(string text)
        {
            return All.Where(c =>
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
            return urls
                .Select(course => _uniwikiContext
                    .Courses
                    .FirstOrDefault(c => c.Url == course.courseUrl && c.StudyGroup.Url == course.studyGroupUrl && c.StudyGroup.University.Url == course.universityUrl))
                .Where(c => c != null);
        }

    }
}

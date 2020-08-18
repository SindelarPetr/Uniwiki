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
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{


    internal class CourseRepository : RemovableRepositoryBase<CourseModel, Guid>, ICourseRepository
    {
        private readonly IStringStandardizationService _stringStandardizationService;
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.Error_CourseNotFound;

        public CourseRepository(UniwikiContext uniwikiContext, IStringStandardizationService stringStandardizationService, TextService textService) : base(uniwikiContext, uniwikiContext.Courses)
        {
            _stringStandardizationService = stringStandardizationService;
            _textService = textService;
        }

        public CourseModel GetCourseFromUrl(string universityUrl, string studyGroupUrl, string courseUrl)
        {
            var neutralizedCourseUrl = courseUrl.Neutralize();
            var neutralizedStudyGroupUrl = studyGroupUrl.Neutralize();
            var neutralizedUniversityUrl = universityUrl.Neutralize();

            // TODO: Optimize this
            return All
                .Where(c => c.Url == courseUrl && c.StudyGroupUrl == neutralizedStudyGroupUrl && c.UniversityUrl == neutralizedUniversityUrl)
                .Include(c => c.StudyGroup)
                .ThenInclude(c => c.University)
                .FirstOrDefault() ?? throw new RequestException(_textService.Error_CourseNotFound);
        }



        public IEnumerable<CourseModel> SearchCourses(string text)
        {
            return All
                .Include(c => c.StudyGroup)
                .ThenInclude(g => g.University)
                .Where(c => c.CodeStandardized.Contains(text) || c.FullNameStandardized.Contains(text));
        }

        public IEnumerable<CourseModel> SearchCoursesFromStudyGroup(string text, StudyGroupModel studyGroup)
        {
            return SearchCourses(text)
                .Where(c => c.StudyGroupId == studyGroup.Id);
        }

        public IEnumerable<CourseModel> SearchCoursesFromUniversity(string text, UniversityModel university)
        {
            return SearchCourses(text).Where(c => c.StudyGroup.UniversityId == university.Id);
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
                .Select(course => All
                    .FirstOrDefault(c => c.Url == course.courseUrl && c.StudyGroup.Url == course.studyGroupUrl && c.StudyGroup.University.Url == course.universityUrl))
                .Where(c => c != null);
        }

        public CourseModel AddCourse(string code, string fullName, ProfileModel author, StudyGroupModel faculty, string universityUrl, string url)
        {
            var codeStandardized = _stringStandardizationService.StandardizeSearchText(code);
            var fullNameStandardized = _stringStandardizationService.StandardizeSearchText(fullName);

            var course = new CourseModel(Guid.NewGuid(), code, codeStandardized, fullName, fullNameStandardized, author, faculty, universityUrl, url, false);

            All.Add(course);

            SaveChanges();

            return course;
        }

        public CourseModel GetCourseWithStudyGroupAndUniversity(Guid courseId)
        => All
            .Where(c => c.Id == courseId)
            .Include(c => c.StudyGroup)
            .ThenInclude(g => g.University)
            .First();
    }
}

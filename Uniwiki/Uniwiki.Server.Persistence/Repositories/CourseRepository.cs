using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Services;
using Shared.Services.Abstractions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;

using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    public class CourseRepository : RepositoryBase<CourseModel, Guid>
    {
        private readonly UniwikiContext _uniwikiContext;
        private readonly StringStandardizationService _stringStandardizationService;
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.Error_CourseNotFound;

        public CourseRepository(UniwikiContext uniwikiContext, StringStandardizationService stringStandardizationService, TextService textService) : base(uniwikiContext, uniwikiContext.Courses)
        {
            _uniwikiContext = uniwikiContext;
            _stringStandardizationService = stringStandardizationService;
            _textService = textService;
        }

        public CourseModel GetCourseFromUrl(string universityUrl, string studyGroupUrl, string courseUrl)
        {
            var neutralizedCourseUrl = courseUrl.Neutralize();
            var neutralizedStudyGroupUrl = studyGroupUrl.Neutralize();
            var neutralizedUniversityUrl = universityUrl.Neutralize();

            // TODO: Use Single everywhere instead of First()
            // TODO: Optimize this
            return All
                .Include(c => c.StudyGroup)
                .ThenInclude(c => c.University)
                .SingleOrDefault(c => c.Url == courseUrl && c.StudyGroupUrl == neutralizedStudyGroupUrl && c.UniversityUrl == neutralizedUniversityUrl) 
                ?? throw new RequestException(_textService.Error_CourseNotFound);
        }

        public IEnumerable<CourseModel> SearchCourses(string text) 
            => All
                .AsNoTracking()
                .Include(c => c.StudyGroup)
                .ThenInclude(g => g.University)
                .Where(c => c.CodeStandardized.Contains(text) || c.FullNameStandardized.Contains(text));

        public bool IsCourseUrlUnique(Guid studyGroupId, string url) 
            => _uniwikiContext
            .Courses
            .Where(c => c.StudyGroupId == studyGroupId).All(c => c.Url != url);

        public bool IsNameUnique(Guid studyGroupId, string name) 
            => _uniwikiContext
            .Courses
            .Where(c => c.StudyGroupId == studyGroupId)
            .All(c => c.LongName.ToLower().Trim() != name.ToLower().Trim());

        public CourseModel AddCourse(string code, string fullName, Guid authorId, Guid facultyId, string universityUrl, string url, string studyGroupUrl)
        {
            var codeStandardized = _stringStandardizationService.StandardizeSearchText(code);
            var fullNameStandardized = _stringStandardizationService.StandardizeSearchText(fullName);

            var course = new CourseModel(Guid.NewGuid(), code, codeStandardized, fullName, fullNameStandardized, authorId, facultyId, universityUrl, url, studyGroupUrl);

            All.Add(course);

            return course;
        }
    }
}

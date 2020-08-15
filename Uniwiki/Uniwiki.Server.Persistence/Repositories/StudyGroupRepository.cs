using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Exceptions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class StudyGroupRepository : RemovableRepositoryBase<StudyGroupModel, Guid>, IStudyGroupRepository
    {
        private readonly IStringStandardizationService _stringStandardizationService;
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.FacultyNotFound;

        public StudyGroupRepository(UniwikiContext uniwikiContext, IStringStandardizationService stringStandardizationService, TextService textService) : base(uniwikiContext, uniwikiContext.StudyGroups)
        {
            _stringStandardizationService = stringStandardizationService;
            _textService = textService;
        }

        public StudyGroupModel GetStudyGroup(string facultyUrlName)
        {
            return TryGetStudyGroup(facultyUrlName) ?? throw new RequestException(_textService.Error_NoFacultyWithUrl(facultyUrlName));
        }

        public IEnumerable<CourseModel> GetCourses(string studyGroupUrl)
        {
            return GetStudyGroup(studyGroupUrl).Courses.OrderBy(c => c.FullName);
        }

        public IEnumerable<StudyGroupModel> SearchStudyGroups(string text)
        {
            return All.Where(g =>
                    _stringStandardizationService.StandardizeSearchText(g.ShortName).Contains(text) ||
                    _stringStandardizationService.StandardizeSearchText(g.LongName).Contains(text));
        }

        public IEnumerable<StudyGroupModel> SearchStudyGroupsOfUniversity(string text, UniversityModel university)
        {
            return SearchStudyGroups(text).Where(g => g.University == university);
        }

        //public StudyGroupModel CreateStudyGroup(UniversityModel university, string shortName, string longName, string url,
        //    ProfileModel profile, Language primaryLanguage)
        //{
        //    // Create ID
        //    var id = Guid.NewGuid();

        //    // Create courses
        //    var courses = _uniwikiContext.Courses.Where(c => c.StudyGroup.Id == id);

        //    // Create the study group
        //    var studyGroup = new StudyGroupModel(id, university, shortName, longName, url, profile, primaryLanguage, courses);

        //    // Add the study group to the DB
        //    _uniwikiContext.StudyGroups.Add(studyGroup);

        //    return studyGroup;
        //}

        public StudyGroupModel TryGetStudyGroup(string url)
        {
            return All.FirstOrDefault(g => g.Url == url);
        }

        public bool IsStudyGroupNameUniq(UniversityModel university, string nameForSearching)
        {
            return All.Where(g => g.UniversityId == university.Id).All(g => g.LongNameForSearching != nameForSearching);
        }

        public StudyGroupModel AddStudyGroup(UniversityModel university, string shortName, string longName, string url, ProfileModel profile, Language primaryLanguage)
        {
            // Create ID
            var id = Guid.NewGuid();

            // Create the study group
            var studyGroup = new StudyGroupModel(id, university, shortName, longName, url, profile, primaryLanguage, false);

            // Add the study group to the DB
            All.Add(studyGroup);

            SaveChanges();

            return studyGroup;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Shared;
using Shared.Exceptions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class StudyGroupRepository : IStudyGroupRepository
    {
        private readonly UniwikiContext _uniwikiContext;
        private readonly IStringStandardizationService _stringStandardizationService;

        public StudyGroupRepository(UniwikiContext uniwikiContext, IStringStandardizationService stringStandardizationService)
        {
            _uniwikiContext = uniwikiContext;
            _stringStandardizationService = stringStandardizationService;
        }

        public StudyGroupModel FindById(Guid id)
        {
            return _uniwikiContext.StudyGroups.FirstOrDefault(g => g.Id == id) ?? throw new RequestException("Couldnt find the study group.");
        }

        public StudyGroupModel GetStudyGroup(string studyGroupUrlName)
        {
            return TryGetStudyGroup(studyGroupUrlName) ?? throw new RequestException($"There is no study group with the url '{ studyGroupUrlName }'");
        }

        public IEnumerable<CourseModel> GetCourses(string studyGroupUrl)
        {
            return GetStudyGroup(studyGroupUrl).Courses.OrderBy(c => c.FullName);
        }

        public IEnumerable<StudyGroupModel> GetStudyGroups()
        {
            return _uniwikiContext.StudyGroups;
        }

        public IEnumerable<StudyGroupModel> SearchStudyGroups(string text)
        {
            return _uniwikiContext.StudyGroups.Where(g =>
                    _stringStandardizationService.StandardizeSearchText(g.ShortName).Contains(text) ||
                    _stringStandardizationService.StandardizeSearchText(g.LongName).Contains(text));
        }

        public IEnumerable<StudyGroupModel> SearchStudyGroupsOfUniversity(string text, UniversityModel university)
        {
            return SearchStudyGroups(text).Where(g => g.University == university);
        }

        public StudyGroupModel CreateStudyGroup(UniversityModel university, string shortName, string longName, string url,
            ProfileModel profile, Language primaryLanguage)
        {
            // Create ID
            var id = Guid.NewGuid();

            // Create courses
            var courses = _uniwikiContext.Courses.Where(c => c.StudyGroup.Id == id);

            // Create the study group
            var studyGroup = new StudyGroupModel(id, university, shortName, longName, url, profile, primaryLanguage, courses);

            // Add the study group to the DB
            _uniwikiContext.StudyGroups.Add(studyGroup);

            return studyGroup;
        }

        public StudyGroupModel TryGetStudyGroup(string url)
        {
            return _uniwikiContext.StudyGroups.FirstOrDefault(g => g.Url == url);
        }

        public bool IsStudyGroupNameUniq(UniversityModel university, string name)
        {
            return university.StudyGroups.All(g => g.LongName.ToLower() != name.ToLower());
        }
    }
}

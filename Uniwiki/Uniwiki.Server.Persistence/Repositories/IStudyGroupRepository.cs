using System;
using System.Collections.Generic;
using Shared;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence.Repositories
{
    public interface IStudyGroupRepository
    {
        StudyGroupModel FindById(Guid id);
        StudyGroupModel GetStudyGroup(string studyGroupUrlName);
        IEnumerable<CourseModel> GetCourses(string studyGroupUrl);
        IEnumerable<StudyGroupModel> GetStudyGroups();
        IEnumerable<StudyGroupModel> SearchStudyGroups(string text);
        IEnumerable<StudyGroupModel> SearchStudyGroupsOfUniversity(string text, UniversityModel university);
        StudyGroupModel CreateStudyGroup(UniversityModel university, string shortName, string longName, string url,
            ProfileModel profile, Language primaryLanguage);
        StudyGroupModel? TryGetStudyGroup(string url);
        bool IsStudyGroupNameUniq(UniversityModel university, string name);
    }
}
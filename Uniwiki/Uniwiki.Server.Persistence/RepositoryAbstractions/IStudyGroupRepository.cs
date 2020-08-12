using System;
using System.Collections.Generic;
using Shared;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface IStudyGroupRepository : IIdRepository<StudyGroupModel, Guid>, IRemovableRepository<StudyGroupModel>
    {
        StudyGroupModel GetStudyGroup(string studyGroupUrlName);
        IEnumerable<CourseModel> GetCourses(string studyGroupUrl);
        IEnumerable<StudyGroupModel> SearchStudyGroups(string text);
        IEnumerable<StudyGroupModel> SearchStudyGroupsOfUniversity(string text, UniversityModel university);
        StudyGroupModel? TryGetStudyGroup(string url);
        bool IsStudyGroupNameUniq(UniversityModel university, string name);
        StudyGroupModel AddStudyGroup(UniversityModel university, string shortName, string longName, string url, ProfileModel profile, Language primaryLanguage);
    }
}
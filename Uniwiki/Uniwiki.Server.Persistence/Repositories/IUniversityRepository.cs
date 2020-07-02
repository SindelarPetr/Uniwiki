using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence.Repositories
{
    public interface IUniversityRepository
    {
        UniversityModel GetUniversityByUrlName(string universityUrlName);
        IEnumerable<UniversityModel> FindUniversities(string text);
        IEnumerable<UniversityModel> GetUniversities();
        IEnumerable<UniversityModel> GetRecentUniversities();
        IEnumerable<StudyGroupModel> GetStudyGroups(string universityUrl);
        IEnumerable<UniversityModel> SearchUniversities(string text);
        UniversityModel FindById(Guid id);
        UniversityModel CreateUniversity(string fullName, string shortName, string url);
        bool IsNameAndUrlUniq(string fullName, string url);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Services;
using Uniwiki.Server.Persistence.InMemory.Services;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.InMemory.Repositories
{
    internal class UniversityInMemoryRepository : IUniversityRepository
    {
        private readonly DataService _dataService;
        private readonly IStringStandardizationService _stringStandardizationService;
        private readonly TextService _textService;

        public UniversityInMemoryRepository(DataService dataService, IStringStandardizationService stringStandardizationService, TextService textService)
        {
            _dataService = dataService;
            _stringStandardizationService = stringStandardizationService;
            _textService = textService;
        }

        public UniversityModel GetUniversityByUrlName(string universityUrlName) => _dataService.Universities.Find(u => u.Url == universityUrlName);

        public IEnumerable<UniversityModel> FindUniversities(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
                return new UniversityModel[0];

            var neutralizedText = text.Neutralize();

            var universities = _dataService.Universities.Where(u => u.FullName.Neutralize().Contains(neutralizedText) || u.ShortName.Neutralize().Contains(neutralizedText));
            return universities;
        }

        public IEnumerable<UniversityModel> GetUniversities() => _dataService.Universities;

        public IEnumerable<UniversityModel> GetRecentUniversities() => new List<UniversityModel>() { _dataService.Universities.First() };

        public IEnumerable<StudyGroupModel> GetStudyGroups(string universityUrl)
        {
            var uni = _dataService.Universities.FirstOrDefault(u => u.Url == universityUrl);

            if(uni == null)
                throw new RequestException( _textService.Error_UniversityNotFound);


            return uni?.StudyGroups;
        }

        public IEnumerable<UniversityModel> SearchUniversities(string text)
        {
            return GetUniversities().Where(u =>
                    _stringStandardizationService.StandardizeSearchText(u.ShortName).Contains(text) ||
                    _stringStandardizationService.StandardizeSearchText(u.FullName).Contains(text));
        }

        public UniversityModel FindById(Guid id)
        {
            return _dataService.Universities.First(u => u.Id == id);
        }

        public UniversityModel CreateUniversity(string fullName, string shortName, string url)
        {
            // Create ID
            var id = Guid.NewGuid();

            // Get study groups
            var studyGroups = _dataService.StudyGroups.Where(g => g.University.Id == id);

            // Create the university
            var university = new UniversityModel(id, fullName, shortName, url, studyGroups);

            // Add the university to the DB
            _dataService.Universities.Add(university);

            return university;
        }

        public bool IsNameAndUrlUniq(string fullName, string url)
        {
            return _dataService.Universities.All(u => u.FullName.ToLower() != fullName.ToLower() && u.Url != url);
        }
    }
}

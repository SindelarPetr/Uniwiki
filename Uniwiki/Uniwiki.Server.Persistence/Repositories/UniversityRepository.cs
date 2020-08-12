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
    internal class UniversityRepository : RepositoryBase<UniversityModel>, IUniversityRepository
    {
        private readonly IStringStandardizationService _stringStandardizationService;
        private readonly TextService _textService;

        public string NotFoundByIdMessage => _textService.Error_UniversityNotFound;

        public UniversityRepository(UniwikiContext uniwikiContext, IStringStandardizationService stringStandardizationService, TextService textService) : base(uniwikiContext, uniwikiContext.Universities)
        {
            _stringStandardizationService = stringStandardizationService;
            _textService = textService;
        }

        public UniversityModel GetUniversityByUrlName(string universityUrlName) => All.First(u => u.Url == universityUrlName);

        public IEnumerable<UniversityModel> FindUniversities(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
                return new UniversityModel[0];

            var neutralizedText = text.Neutralize();

            var universities = All.Where(u => u.FullName.Neutralize().Contains(neutralizedText) || u.ShortName.Neutralize().Contains(neutralizedText));
            return universities;
        }

        public IEnumerable<UniversityModel> GetUniversities() => All;

        public IEnumerable<StudyGroupModel> GetStudyGroups(string universityUrl)
        {
            var uni = All.FirstOrDefault(u => u.Url == universityUrl);

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

        public bool IsNameAndUrlUniq(string fullName, string url)
        {
            return All.All(u => u.FullName.ToLower() != fullName.ToLower() && u.Url != url);
        }

        public UniversityModel AddUniversity(string fullName, string shortName, string url)
        {
            var university = new UniversityModel(Guid.NewGuid(), fullName, shortName, url, false);

            All.Add(university);

            return university;
        }
    }
}

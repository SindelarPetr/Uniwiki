using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Exceptions;
using Shared.Services;
using Shared.Services.Abstractions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;

using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    public class StudyGroupRepository : RepositoryBase<StudyGroupModel, Guid> 
    {
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.FacultyNotFound;

        public StudyGroupRepository(UniwikiContext uniwikiContext, StringStandardizationService stringStandardizationService, TextService textService) : base(uniwikiContext, uniwikiContext.StudyGroups)
        {
            _textService = textService;
        }

        public StudyGroupModel TryGetStudyGroup(string url)
            => All.FirstOrDefault(g => g.Url == url);

        public bool IsStudyGroupNameUniq(Guid universityId, string nameForSearching) 
            => All.Where(g => g.UniversityId == universityId).All(g => g.LongNameForSearching != nameForSearching);

        public IQueryable<StudyGroupModel> AddStudyGroup(Guid universityId, string shortName, string longName, string url, Language primaryLanguage)
        {
            // Create ID
            var id = Guid.NewGuid();

            // Create the study group
            var studyGroup = new StudyGroupModel(id, universityId, shortName, longName, url, primaryLanguage);

            // Add the study group to the DB
            All.Add(studyGroup);

            return UniwikiContext.StudyGroups.Where(g => g.Id == studyGroup.Id);
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Shared.Services;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class AddStudyGroupServerAction : ServerActionBase<AddStudyGroupRequestDto, AddStudyGroupResponseDto>
    {
        private readonly UniversityRepository _universityRepository;
        private readonly StudyGroupRepository _studyGroupRepository;
        private readonly StringStandardizationService _stringStandardizationService;
        private readonly TextService _textService;
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.Admin;

        public AddStudyGroupServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, UniversityRepository universityRepository, StudyGroupRepository studyGroupRepository, StringStandardizationService stringStandardizationService, TextService textService) : base(serviceProvider)
        {
            _universityRepository = universityRepository;
            _studyGroupRepository = studyGroupRepository;
            _stringStandardizationService = stringStandardizationService;
            _textService = textService;
        }

        protected override Task<AddStudyGroupResponseDto> ExecuteAsync(AddStudyGroupRequestDto request, RequestContext context)
        {
            var longName = request.StudyGroupName;
            var shortName = request.StudyGroupShortcut;
            var universityId = request.UniversityId;

            // Check if the name of the group is uniq
            if(!_studyGroupRepository.IsStudyGroupNameUniq(universityId, longName))
            {
                throw new RequestException(_textService.Error_StudyGroupNameIsTaken(longName));
            }

            // Create url for the study group
            var studyGroupUrl = _stringStandardizationService.CreateUrl(shortName, url => _studyGroupRepository.TryGetStudyGroup(url) == null);

            // Create the study group
            var studyGroup = _studyGroupRepository.AddStudyGroup(universityId, shortName, longName, studyGroupUrl, request.PrimaryLanguage);

            // Create study group DTO
            // var studyGroupDto = studyGroup.;

            // Create the response
            var response = studyGroup.Include(g => g.University).Select(g => new AddStudyGroupResponseDto(g.Id/*, g.ShortName, g.University.LongName, g.University.Url*/)).Single();

            return Task.FromResult(response);
        }

    }
}

using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class AddStudyGroupServerAction : ServerActionBase<AddStudyGroupRequestDto, AddStudyGroupResponseDto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IStudyGroupRepository _studyGroupRepository;
        private readonly IStringStandardizationService _stringStandardizationService;
        private readonly TextService _textService;
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.Admin;

        public AddStudyGroupServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository, IUniversityRepository universityRepository, IStudyGroupRepository studyGroupRepository, IStringStandardizationService stringStandardizationService, TextService textService) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
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

            // Get profile
            var profile = _profileRepository.FindById(context.User.Id);
            
            // Get university
            var university = _universityRepository.FindById(universityId);

            // Check if the name of the group is uniq
            if(!_studyGroupRepository.IsStudyGroupNameUniq(university, longName))
                throw new RequestException(_textService.Error_StudyGroupNameIsTaken(longName));

            // Create url for the study group
            var studyGroupUrl = _stringStandardizationService.CreateUrl(shortName, url => _studyGroupRepository.TryGetStudyGroup(url) == null);

            // Create the study group
            var studyGroup = new StudyGroupModel(Guid.NewGuid(), university, shortName, longName, studyGroupUrl, profile, request.PrimaryLanguage, false);
            _studyGroupRepository.Add(studyGroup);

            // Create study group DTO
            var studyGroupDto = studyGroup.ToDto();

            // Create the response
            var response = new AddStudyGroupResponseDto(studyGroupDto);

            return Task.FromResult(response);
        }

    }
}

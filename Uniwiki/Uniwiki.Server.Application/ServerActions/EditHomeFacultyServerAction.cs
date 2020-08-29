using Server.Appliaction.ServerActions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class EditHomeFacultyServerAction : ServerActionBase<EditHomeFacultyRequestDto, EditHomeFacultyResponseDto>
    {
        private readonly ProfileRepository _profileRepository;
        private readonly StudyGroupRepository _studyGroupRepository;

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.PrimaryToken;

        public EditHomeFacultyServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, StudyGroupRepository studyGroupRepository) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _studyGroupRepository = studyGroupRepository;
        }

        protected override Task<EditHomeFacultyResponseDto> ExecuteAsync(EditHomeFacultyRequestDto request, RequestContext context)
        {
            
            // Edit the home faculty of the user
            var updatedProfile = _profileRepository.EditHomeFaculty(context.UserId!.Value, request.FacultyId);

            var profileDto = updatedProfile.ToAuthorizedUser().Single();

            // Create response
            var response = new EditHomeFacultyResponseDto(profileDto);

            return Task.FromResult(response);
        }
    }
}

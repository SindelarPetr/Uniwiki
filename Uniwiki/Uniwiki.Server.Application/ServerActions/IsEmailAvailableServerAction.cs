using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{

    public class IsEmailAvailableServerAction : ServerActionBase<IsEmailAvailableRequestDto, IsEmailAvailableResponseDto>
    {
        private readonly IProfileRepository _profileRepository;

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        public IsEmailAvailableServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository)
            :base(serviceProvider)
        {
            _profileRepository = profileRepository;
        }

        protected override Task<IsEmailAvailableResponseDto> ExecuteAsync(IsEmailAvailableRequestDto request, RequestContext requestContext)
        {
            // Check the availibility of the email
            var isAvailable = _profileRepository.TryGetProfileByEmail(request.Email) == null;

            // Create the response
            var response = new IsEmailAvailableResponseDto(isAvailable);

            return Task.FromResult(response);
        }
    }
}

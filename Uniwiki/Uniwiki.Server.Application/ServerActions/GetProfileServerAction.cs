using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class GetProfileServerAction : ServerActionBase<GetProfileRequest, GetProfileResponse>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;
        private readonly IProfileRepository _profileRepository;

        public GetProfileServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository):base(serviceProvider)
        {
            _profileRepository = profileRepository;
        }

        protected override Task<GetProfileResponse> ExecuteAsync(GetProfileRequest request, RequestContext context)
        {
            var profile = _profileRepository.GetProfileByUrl(request.NameIdentifier);
            return Task.FromResult(new GetProfileResponse(profile.ToDto()));
        }
    }
}

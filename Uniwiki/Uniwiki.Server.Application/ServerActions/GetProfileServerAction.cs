using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class GetProfileServerAction : ServerActionBase<GetProfileRequest, GetProfileResponse>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;
        private readonly ProfileRepository _profileRepository;
        private readonly UniwikiContext _uniwikiContext;

        public GetProfileServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<GetProfileResponse> ExecuteAsync(GetProfileRequest request, RequestContext context)
        {
            // Get the wanted profile
            var profile = _uniwikiContext.Profiles.ToViewModel(context.UserId).Single(p => p.Url == request.Url);

            // Determine if the requesting user matches the authenticated user
            var isAuthenticated = context.UserId == profile.Id;

            // Create the response 
            var response = new GetProfileResponse(profile, isAuthenticated);

            return Task.FromResult(response);
        }
    }
}

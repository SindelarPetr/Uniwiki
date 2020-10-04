using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class GetProfileServerAction : ServerActionBase<GetProfileRequest, GetProfileResponse>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;
        private readonly ProfileRepository _profileRepository;
        private readonly UniwikiContext _uniwikiContext;
        private readonly TextService _textService;

        public GetProfileServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, UniwikiContext uniwikiContext, TextService textService) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _uniwikiContext = uniwikiContext;
            _textService = textService;
        }

        protected override Task<GetProfileResponse> ExecuteAsync(GetProfileRequest request, RequestContext context)
        {
            // Get the wanted profile
            var profile = _uniwikiContext
                .Profiles
                .Where(p => p.Url == request.Url)
                .ToProfileViewModel()
                 ?? throw  new RequestException(_textService.UserNotFound);

            // Determine if the requesting user matches the authenticated user
            AuthorizedUserDto? authorizedUser = null;
            if (context.UserId == profile.Id)
            {
                authorizedUser = _uniwikiContext.Profiles.Where(p => p.Url == request.Url).ToAuthorizedUserDto();
            }

            // Create the response 
            var response = new GetProfileResponse(profile, authorizedUser);

            return Task.FromResult(response);
        }
    }
}

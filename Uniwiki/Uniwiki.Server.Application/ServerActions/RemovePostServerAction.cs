using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class RemovePostServerAction : ServerActionBase<RemovePostRequestDto, RemovePostResponseDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IProfileRepository _profileRepository;
        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public RemovePostServerAction(IServiceProvider serviceProvider, IPostRepository postRepository, IProfileRepository profileRepository) : base(serviceProvider)
        {
            _postRepository = postRepository;
            _profileRepository = profileRepository;
        }

        protected override Task<RemovePostResponseDto> ExecuteAsync(RemovePostRequestDto request, RequestContext context)
        {
            // Get profile
            var profile = _profileRepository.FindById(context.User.Id);

            // Get post
            var post = _postRepository.FindById(request.PostId);

            // Verify if the post belongs to the user, who is removing it
            if (profile != post.Author)
            {
                throw new RequestException("Its not possible to remove a post not belonging to the authorized author.");
            }

            // Remove the post
            _postRepository.RemovePost(post);

            // Create response
            var response = new RemovePostResponseDto();

            return Task.FromResult(response);
        }
    }
}

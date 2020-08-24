using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class RemovePostServerAction : ServerActionBase<RemovePostRequestDto, RemovePostResponseDto>
    {
        private readonly PostRepository _postRepository;
        private readonly ProfileRepository _profileRepository;
        private readonly TextService _textService;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public RemovePostServerAction(IServiceProvider serviceProvider, PostRepository postRepository, ProfileRepository profileRepository, TextService textService) : base(serviceProvider)
        {
            _postRepository = postRepository;
            _profileRepository = profileRepository;
            _textService = textService;
        }

        protected override Task<RemovePostResponseDto> ExecuteAsync(RemovePostRequestDto request, RequestContext context)
        {
            // Get profile
            var profile = _profileRepository.FindById(context.User!.Id);

            // Get post
            var post = _postRepository.FindById(request.PostId);

            // Verify if the post belongs to the user, who is removing it, or that the user is admin
            if (profile != post.Author && profile.AuthenticationLevel != AuthenticationLevel.Admin)
            {
                throw new RequestException(_textService.RemovePost_CannotRemoveNonOwnersPost);
            }

            // Remove the post
            _postRepository.Remove(post);

            // Create response
            var response = new RemovePostResponseDto();

            return Task.FromResult(response);
        }
    }
}

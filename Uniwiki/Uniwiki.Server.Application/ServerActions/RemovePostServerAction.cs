using System;
using System.Linq;
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
        private readonly UniwikiContext _uniwikiContext;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public RemovePostServerAction(IServiceProvider serviceProvider, PostRepository postRepository, ProfileRepository profileRepository, TextService textService, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _postRepository = postRepository;
            _profileRepository = profileRepository;
            _textService = textService;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<RemovePostResponseDto> ExecuteAsync(RemovePostRequestDto request, RequestContext context)
        {
            // Get post
            var post = _uniwikiContext.Posts.Find(request.PostId) ?? throw new RequestException(_textService.CouldNotFindPost_Removing);

            // Verify if the post belongs to the user, who is removing it, or that the user is admin
            if (context.UserId != post.AuthorId)
            {
                throw new RequestException(_textService.RemovePost_CannotRemoveNonOwnersPost);
            }

            // Remove the post
            _uniwikiContext.Posts.Remove(post);

            // Create response
            var response = new RemovePostResponseDto();

            return Task.FromResult(response);
        }
    }
}

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
    internal class UnlikePostServerAction : ServerActionBase<UnlikePostRequestDto, UnlikePostResponseDto>
    {
        private readonly ProfileRepository _profileRepository;
        private readonly PostRepository _postRepository;
        private readonly PostLikeRepository _postLikeRepository;
        private readonly UniwikiContext _uniwikiContext;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public UnlikePostServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, PostRepository postRepository, PostLikeRepository postLikeRepository, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postRepository = postRepository;
            _postLikeRepository = postLikeRepository;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<UnlikePostResponseDto> ExecuteAsync(UnlikePostRequestDto request, RequestContext context)
        {
            // Like the post
            _postLikeRepository.UnlikePost(request.PostId, context.UserId!.Value);

            // Find the updated post
            var updatedPost = _uniwikiContext.Posts.Where(p => p.Id == request.PostId).ToDto(context.UserId).Single();

            // Create result
            var result = new UnlikePostResponseDto(updatedPost);

            return Task.FromResult(result);

        }
    }
}
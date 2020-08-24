using System;
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

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public UnlikePostServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, PostRepository postRepository, PostLikeRepository postLikeRepository) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postRepository = postRepository;
            _postLikeRepository = postLikeRepository;
        }

        protected override Task<UnlikePostResponseDto> ExecuteAsync(UnlikePostRequestDto request, RequestContext context)
        {
            // Get post
            var post = _postRepository.FindById(request.PostId);

            // Like the post
            _postLikeRepository.UnlikePost(post, context.User);

            // Create result
            var result = new UnlikePostResponseDto(post.ToDto(context.User));

            return Task.FromResult(result);

        }
    }
}
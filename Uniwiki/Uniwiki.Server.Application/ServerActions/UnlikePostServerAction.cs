using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class UnlikePostServerAction : ServerActionBase<UnlikePostRequestDto, UnlikePostResponseDto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPostRepository _postRepository;
        private readonly IPostLikeRepository _postLikeRepository;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public UnlikePostServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository, IPostRepository postRepository, IPostLikeRepository postLikeRepository) : base(serviceProvider)
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
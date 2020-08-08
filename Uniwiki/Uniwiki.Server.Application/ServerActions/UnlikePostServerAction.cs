using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class UnlikePostServerAction : ServerActionBase<UnlikePostRequestDto, UnlikePostResponseDto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPostRepository _postRepository;
        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public UnlikePostServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository, IPostRepository postRepository) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postRepository = postRepository;
        }

        protected override Task<UnlikePostResponseDto> ExecuteAsync(UnlikePostRequestDto request, RequestContext context)
        {
            // Get profile
            var profile = _profileRepository.FindById(context.User.Id);

            // Get post
            var post = _postRepository.FindById(request.PostId);

            // Like the post
            _postRepository.UnlikePost(post, profile);

            // Create result
            var result = new UnlikePostResponseDto(post.ToDto(profile));

            return Task.FromResult(result);

        }
    }
}
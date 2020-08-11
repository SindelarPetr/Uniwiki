using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class LikePostServerAction : ServerActionBase<LikePostRequestDto, LikePostResponseDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly ITimeService _timeService;
        private readonly IPostLikeRepository _postLikeRepository;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public LikePostServerAction(IServiceProvider serviceProvider, IPostRepository postRepository, IProfileRepository profileRepository, ITimeService timeService, IPostLikeRepository postLikeRepository) : base(serviceProvider)
        {
            _postRepository = postRepository;
            _profileRepository = profileRepository;
            _timeService = timeService;
            _postLikeRepository = postLikeRepository;
        }

        protected override Task<LikePostResponseDto> ExecuteAsync(LikePostRequestDto request, RequestContext context)
        {
            // Get profile
            var profile = _profileRepository.FindById(context.User.Id);

            // Get post
            var post = _postRepository.FindById(request.PostId);

            // Like the post
            _postLikeRepository.LikePost(post, profile, _timeService.Now);

            // Create result
            var result = new LikePostResponseDto(post.ToDto(profile));

            return Task.FromResult(result);
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class LikePostServerAction : ServerActionBase<LikePostRequestDto, LikePostResponseDto>
    {
        private readonly PostRepository _postRepository;
        private readonly ProfileRepository _profileRepository;
        private readonly ITimeService _timeService;
        private readonly PostLikeRepository _postLikeRepository;
        private readonly UniwikiContext _uniwikiContext;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public LikePostServerAction(IServiceProvider serviceProvider, PostRepository postRepository, ProfileRepository profileRepository, ITimeService timeService, PostLikeRepository postLikeRepository, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _postRepository = postRepository;
            _profileRepository = profileRepository;
            _timeService = timeService;
            _postLikeRepository = postLikeRepository;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<LikePostResponseDto> ExecuteAsync(LikePostRequestDto request, RequestContext context)
        {
            // Like the post
            _postLikeRepository.LikePost(request.PostId, context.UserId!.Value, _timeService.Now);

            // Reload the post
            var postDto = _uniwikiContext.Posts.Where(p => p.Id == request.PostId).ToPostViewModel(context.UserId).Single();

            // Create result
            var result = new LikePostResponseDto(postDto);

            return Task.FromResult(result);
        }
    }
}

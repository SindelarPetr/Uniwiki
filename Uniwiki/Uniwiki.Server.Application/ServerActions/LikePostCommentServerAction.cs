using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class LikePostCommentServerAction : ServerActionBase<LikePostCommentRequestDto, LikePostCommentResponseDto>
    {
        private readonly ProfileRepository _profileRepository;
        private readonly PostCommentRepository _postCommentRepository;
        private readonly ITimeService _timeService;
        private readonly PostCommentLikeRepository _postCommentLikeRepository;
        private readonly TextService _textService;
        private readonly UniwikiContext _uniwikiContext;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public LikePostCommentServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, PostCommentRepository postCommentRepository, ITimeService timeService, PostCommentLikeRepository postCommentLikeRepository, TextService textService, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postCommentRepository = postCommentRepository;
            _timeService = timeService;
            _postCommentLikeRepository = postCommentLikeRepository;
            _textService = textService;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<LikePostCommentResponseDto> ExecuteAsync(LikePostCommentRequestDto request, RequestContext context)
        {
            // Like the comment
            var postId = _postCommentLikeRepository.LikeComment(request.PostCommentId, context.UserId!.Value, _timeService.Now);

            // Find the post
            var updatedPost = _uniwikiContext.Posts.ToDto(context.UserId).Single(p => p.Id == postId);

            // Create response
            var response = new LikePostCommentResponseDto(updatedPost);

            return Task.FromResult(response);
        }
    }
}
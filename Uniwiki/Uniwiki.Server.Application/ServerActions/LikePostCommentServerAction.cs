using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class LikePostCommentServerAction : ServerActionBase<LikePostCommentRequestDto, LikePostCommentResponseDto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPostCommentRepository _postCommentRepository;
        private readonly ITimeService _timeService;
        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public LikePostCommentServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository, IPostCommentRepository postCommentRepository, ITimeService timeService) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postCommentRepository = postCommentRepository;
            _timeService = timeService;
        }

        protected override Task<LikePostCommentResponseDto> ExecuteAsync(LikePostCommentRequestDto request, RequestContext context)
        {
            // Get profile
            var profile = _profileRepository.FindById(context.User.Id);

            // Get the comment to like
            var comment = _postCommentRepository.FindById(request.PostCommentId);

            // Like the comment
            _postCommentRepository.LikeComment(comment, profile, _timeService.Now);

            // Create response
            var response = new LikePostCommentResponseDto(comment.Post.ToDto(profile));

            return Task.FromResult(response);
        }
    }
}
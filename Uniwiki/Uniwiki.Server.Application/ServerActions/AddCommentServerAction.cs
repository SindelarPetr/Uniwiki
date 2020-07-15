using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class AddCommentServerAction : ServerActionBase<AddCommentRequestDto, AddCommentResponseDto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPostRepository _postRepository;
        private readonly IPostCommentRepository _postCommentRepository;
        private readonly ITimeService _timeService;
        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public AddCommentServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository, IPostRepository postRepository, IPostCommentRepository postCommentRepository, ITimeService timeService) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postRepository = postRepository;
            _postCommentRepository = postCommentRepository;
            _timeService = timeService;
        }

        protected override Task<AddCommentResponseDto> ExecuteAsync(AddCommentRequestDto request, RequestContext context)
        {
            // Get profile
            var profile = _profileRepository.FindById(context.User.Id);

            // Get post
            var post = _postRepository.FindById(request.PostId);

            // Create the comment
            _postCommentRepository.CreatePostComment(profile, post, request.CommentText, _timeService.Now);

            // Create response
            var response = new AddCommentResponseDto(post.ToDto(profile));

            return Task.FromResult(response);
        }
    }
}
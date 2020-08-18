using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class AddCommentServerAction : ServerActionBase<AddCommentRequestDto, AddCommentResponseDto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPostRepository _postRepository;
        private readonly IPostCommentRepository _postCommentRepository;
        private readonly ITimeService _timeService;
        private readonly TextService _textService;

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.PrimaryToken;

        public AddCommentServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository, IPostRepository postRepository, IPostCommentRepository postCommentRepository, ITimeService timeService, TextService textService) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postRepository = postRepository;
            _postCommentRepository = postCommentRepository;
            _timeService = timeService;
            _textService = textService;
        }

        protected override Task<AddCommentResponseDto> ExecuteAsync(AddCommentRequestDto request, RequestContext context)
        {
            // Get post
            var post = _postRepository.FindById(request.PostId, _textService.Error_PostNotFound);

            // Create the comment
            _postCommentRepository.AddPostComment(context.User!, post, request.CommentText, _timeService.Now);

            // Get the updated post
            post = _postRepository.FindById

            // Create response
            var response = new AddCommentResponseDto(post.ToDto(context.User));

            return Task.FromResult(response);
        }
    }
}
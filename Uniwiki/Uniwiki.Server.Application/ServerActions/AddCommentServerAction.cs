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
        private readonly UniwikiContext _uniwikiContext;

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.PrimaryToken;

        public AddCommentServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository, IPostRepository postRepository, IPostCommentRepository postCommentRepository, ITimeService timeService, TextService textService, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postRepository = postRepository;
            _postCommentRepository = postCommentRepository;
            _timeService = timeService;
            _textService = textService;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<AddCommentResponseDto> ExecuteAsync(AddCommentRequestDto request, RequestContext context)
        {
            // Get post
            var post = _postRepository.FindById(request.PostId, _textService.Error_PostNotFound);

            // Create the comment
            _postCommentRepository.AddPostComment(context.User!, post, request.CommentText, _timeService.Now);

            // Get the updated post // TODO: THERE WILL BE A PROBLEM, BECAUSE THE INCLUDES ARE MISSING HERE
            post = _uniwikiContext.Posts.Find(post.Id);

            // Create response
            var response = new AddCommentResponseDto(post.ToDto(context.User));

            return Task.FromResult(response);
        }
    }
}
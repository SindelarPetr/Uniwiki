using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class UnlikePostCommentServerAction : ServerActionBase<UnlikePostCommentRequestDto, UnlikePostCommentResponseDto>
    {
        private readonly PostCommentLikeRepository _postCommentLikeRepository;
        private readonly UniwikiContext _uniwikiContext;
        private readonly PostCommentRepository _postCommentRepository;
        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public UnlikePostCommentServerAction(IServiceProvider serviceProvider, PostCommentRepository postCommentRepository, PostCommentLikeRepository postCommentLikeRepository, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _postCommentRepository = postCommentRepository;
            _postCommentLikeRepository = postCommentLikeRepository;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<UnlikePostCommentResponseDto> ExecuteAsync(UnlikePostCommentRequestDto request, RequestContext context)
        {
            // Unlike it
            var postId = _postCommentLikeRepository.UnlikeComment(request.PostCommentId, context.UserId!.Value);
            
            // Find the post
            var post = _uniwikiContext.Posts.ToDto(context.UserId).Single(p => p.Id == postId);

            // Create response
            var response = new UnlikePostCommentResponseDto(post);

            return Task.FromResult(response);
        }
    }
}
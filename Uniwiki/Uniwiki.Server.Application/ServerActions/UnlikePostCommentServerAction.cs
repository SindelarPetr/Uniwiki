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
    internal class UnlikePostCommentServerAction : ServerActionBase<UnlikePostCommentRequestDto, UnlikePostCommentResponseDto>
    {
        private readonly IPostCommentLikeRepository _postCommentLikeRepository;
        private readonly IPostCommentRepository _postCommentRepository;
        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public UnlikePostCommentServerAction(IServiceProvider serviceProvider, IPostCommentRepository postCommentRepository, IPostCommentLikeRepository postCommentLikeRepository) : base(serviceProvider)
        {
            _postCommentRepository = postCommentRepository;
            _postCommentLikeRepository = postCommentLikeRepository;
        }

        protected override Task<UnlikePostCommentResponseDto> ExecuteAsync(UnlikePostCommentRequestDto request, RequestContext context)
        {
            // Get the comment
            var comment = _postCommentRepository.FindById(request.PostCommentId);

            // Unlike it
            _postCommentLikeRepository.UnlikeComment(comment, context.User);

            // Create response
            var response = new UnlikePostCommentResponseDto(comment.Post.ToDto(context.User));

            return Task.FromResult(response);
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class RemovePostCommentServerAction : ServerActionBase<RemovePostCommentRequestDto, RemovePostCommentResponseDto>
    {
        private readonly ProfileRepository _profileRepository;
        private readonly PostCommentRepository _postCommentRepository;
        private readonly UniwikiContext _uniwikiContext;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public RemovePostCommentServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, PostCommentRepository postCommentRepository, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postCommentRepository = postCommentRepository;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<RemovePostCommentResponseDto> ExecuteAsync(RemovePostCommentRequestDto request, RequestContext context)
        {
            // Get the comment to remove
            var comment = _postCommentRepository.FindById(request.PostCommentId).Single();

            // Check if user is removing his own comment
            if (comment.AuthorId != context.UserId)
            {
                throw new RequestException("You cannot remove a comment which is not yours.");
            }

            // Remove the comment
            _postCommentRepository.Remove(comment);

            // Reload the post with the removed comment
            var post = _uniwikiContext
                .Posts
                 .ToDto(context.UserId)
                 .Single(p => p.Id == comment.PostId); // TODO: Check the performance!

            // Create the response
            var response = new RemovePostCommentResponseDto(post);

            return Task.FromResult(response);
        }
    }
}
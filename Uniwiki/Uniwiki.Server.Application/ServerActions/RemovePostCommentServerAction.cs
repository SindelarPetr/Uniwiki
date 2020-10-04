using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        private readonly UniwikiContext _uniwikiContext;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public RemovePostCommentServerAction(IServiceProvider serviceProvider, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<RemovePostCommentResponseDto> ExecuteAsync(RemovePostCommentRequestDto request, RequestContext context)
        {
            // Get the comment to remove
            var comment = _uniwikiContext.PostComments.Find(request.PostCommentId) 
                          ?? throw new RequestException(string.Empty);

            // Check if user is removing his own comment
            if (comment.AuthorId != context.UserId)
            {
                throw new RequestException("You cannot remove a comment which is not yours.");
            }

            // Remove the comment
            _uniwikiContext.PostComments.Remove(comment);

            _uniwikiContext.SaveChanges();

            _uniwikiContext.Entry(comment).State = EntityState.Detached;

            // Reload the post with the removed comment
            var post = _uniwikiContext
                .Posts
                .Where(p => p.Id == comment.PostId)
                .ToPostViewModel(context.UserId)
                 .Single();

            // Create the response
            var response = new RemovePostCommentResponseDto(post);

            return Task.FromResult(response);
        }
    }
}
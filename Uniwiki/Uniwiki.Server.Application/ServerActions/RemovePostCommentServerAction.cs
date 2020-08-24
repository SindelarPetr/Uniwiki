using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class RemovePostCommentServerAction : ServerActionBase<RemovePostCommentRequestDto, RemovePostCommentResponseDto>
    {
        private readonly ProfileRepository _profileRepository;
        private readonly PostCommentRepository _postCommentRepository;
        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public RemovePostCommentServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, PostCommentRepository postCommentRepository) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postCommentRepository = postCommentRepository;
        }

        protected override Task<RemovePostCommentResponseDto> ExecuteAsync(RemovePostCommentRequestDto request, RequestContext context)
        {
            // Get the comment to remove
            var comment = _postCommentRepository.FindById(request.PostCommentId);

            // Check if user is removing his own comment
            if(comment.Profile != context.User!)
                throw new RequestException("You cannot remove a comment which is not yours.");
            
            // Remove the comment
            _postCommentRepository.Remove(comment);
            
            // Create the response
            var response = new RemovePostCommentResponseDto(comment.Post.ToDto(context.User));

            return Task.FromResult(response);
        }
    }
}
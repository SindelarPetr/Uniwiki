using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class RemovePostCommentServerAction : ServerActionBase<RemovePostCommentRequestDto, RemovePostCommentResponseDto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPostCommentRepository _postCommentRepository;
        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public RemovePostCommentServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository, IPostCommentRepository postCommentRepository) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postCommentRepository = postCommentRepository;
        }

        protected override Task<RemovePostCommentResponseDto> ExecuteAsync(RemovePostCommentRequestDto request, RequestContext context)
        {
            // Get profile
            var profile = _profileRepository.FindById(context.User.Id);

            // Get the comment to remove
            var comment = _postCommentRepository.FindById(request.PostCommentId);

            // Check if user is removing his own comment
            if(comment.Profile != profile)
                throw new RequestException("You cannot remove a comment which is not yours.");
            
            // Remove the comment
            _postCommentRepository.RemoveComment(comment);
            
            // Create the response
            var response = new RemovePostCommentResponseDto(comment.Post.ToDto(profile));

            return Task.FromResult(response);
        }
    }
}
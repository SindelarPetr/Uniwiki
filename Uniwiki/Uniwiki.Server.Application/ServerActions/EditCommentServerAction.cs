using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class EditCommentServerAction : ServerActionBase<EditCommentRequestDto, EditCommentResponseDto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPostCommentRepository _postCommentRepository;
        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public EditCommentServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository, IPostCommentRepository postCommentRepository) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postCommentRepository = postCommentRepository;
        }

        protected override Task<EditCommentResponseDto> ExecuteAsync(EditCommentRequestDto request, RequestContext context)
        {
            // Get the user
            var profile = _profileRepository.FindById(context.User.Id);

            // Get the comment
            var comment = _postCommentRepository.FindById(request.PostCommentId);

            // Check if the user is editting his own comment
            if(profile != comment.Profile)
                throw new RequestException("You cannot remove someone else's comment!");

            // Edit the comment
            var edittedComment = _postCommentRepository.EditComment(comment, request.Text);
            
            // Create result
            var result = new EditCommentResponseDto(edittedComment.ToDto(profile));

            return Task.FromResult(result);
        }
    }
}
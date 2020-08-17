using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class EditCommentServerAction : ServerActionBase<EditCommentRequestDto, EditCommentResponseDto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPostCommentRepository _postCommentRepository;
        private readonly TextService _textService;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public EditCommentServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository, IPostCommentRepository postCommentRepository, TextService textService) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postCommentRepository = postCommentRepository;
            _textService = textService;
        }

        protected override Task<EditCommentResponseDto> ExecuteAsync(EditCommentRequestDto request, RequestContext context)
        {
            // Get the comment
            var comment = _postCommentRepository.FindById(request.PostCommentId);

            // Check if the user is editting his own comment
            if(context.User! != comment.Profile)
                throw new RequestException(_textService.EditComment_YouCannotRemoveSomeoneElsesComment);

            // Edit the comment
            var edittedComment = _postCommentRepository.EditComment(comment, request.Text);
            
            // Create result
            var result = new EditCommentResponseDto(edittedComment.ToDto(context.User));

            return Task.FromResult(result);
        }
    }
}
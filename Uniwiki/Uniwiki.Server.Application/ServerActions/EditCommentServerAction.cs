using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class EditCommentServerAction : ServerActionBase<EditCommentRequestDto, EditCommentResponseDto>
    {
        private readonly ProfileRepository _profileRepository;
        private readonly PostCommentRepository _postCommentRepository;
        private readonly TextService _textService;
        private readonly PostRepository _postRepository;
        private readonly UniwikiContext _uniwikiContext;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public EditCommentServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, PostCommentRepository postCommentRepository, TextService textService, PostRepository postRepository, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postCommentRepository = postCommentRepository;
            _textService = textService;
            _postRepository = postRepository;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<EditCommentResponseDto> ExecuteAsync(EditCommentRequestDto request, RequestContext context)
        {
            // Get the comment
            var comment = _uniwikiContext.PostComments.Single(c => c.Id == request.PostCommentId);

            // Check if the user is editting his own comment
            if(context.UserId != comment.AuthorId)
            {
                throw new RequestException(_textService.EditComment_YouCannotRemoveSomeoneElsesComment);
            }

            // Edit the comment
            _postCommentRepository.EditComment(comment, request.Text);

            throw new NotImplementedException(); // Just finish this once it will be needed

            // Get the updated comment
            // var updatedPost = _postRepository.FindById(comment.PostId);

            // Make DTO
            // var postDto = updatedPost.ToLoginTokenDto(context.UserId).Single();

            // Create result
            // var result = new EditCommentResponseDto();

            //return Task.FromResult(result);
        }
    }
}
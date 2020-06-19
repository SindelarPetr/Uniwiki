using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class UnlikePostCommentServerAction : ServerActionBase<UnlikePostCommentRequestDto, UnlikePostCommentResponseDto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPostCommentRepository _postCommentRepository;
        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public UnlikePostCommentServerAction(IServiceProvider serviceProvider, IPostCommentRepository postCommentRepository, IProfileRepository profileRepository) : base(serviceProvider)
        {
            _postCommentRepository = postCommentRepository;
            _profileRepository = profileRepository;
        }

        protected override Task<UnlikePostCommentResponseDto> ExecuteAsync(UnlikePostCommentRequestDto request, RequestContext context)
        {
            var profile = _profileRepository.FindById(context.User.Id);

            var comment = _postCommentRepository.FindById(request.PostCommentId);

            _postCommentRepository.UnlikeComment(comment, profile);

            var response = new UnlikePostCommentResponseDto(comment.Post.ToDto(profile));

            return Task.FromResult(response);
        }
    }
}
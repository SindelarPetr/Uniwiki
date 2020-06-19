using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class EditPostServerAction : ServerActionBase<EditPostRequestDto, EditPostResponseDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly TextService _textService;
        private readonly IPostTypeRepository _postTypeRepository;
        private readonly IPostFileRepository _postFileRepository;
        private readonly IProfileRepository _profileRepository;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public EditPostServerAction(IServiceProvider serviceProvider, IPostRepository postRepository, TextService textService, IPostTypeRepository postTypeRepository, IPostFileRepository postFileRepository, IProfileRepository profileRepository) :base (serviceProvider)
        {
            _postRepository = postRepository;
            _textService = textService;
            _postTypeRepository = postTypeRepository;
            _postFileRepository = postFileRepository;
            _profileRepository = profileRepository;
        }

        protected override Task<EditPostResponseDto> ExecuteAsync(EditPostRequestDto request, RequestContext context)
        {
            // Get post Id
            var postId = request.PostId;

            // Get post to edit
            var post = _postRepository.FindById(postId);
            var profile = _profileRepository.FindById(context.User.Id);
            var filesForSearch = request.PostFiles.Select(f => (f.Id, f.OriginalName));
            var postFiles = _postFileRepository.FindPostFilesAndUpdateNames(filesForSearch, profile).ToArray();

            // Check if the post belongs to the right user
            if (post.Author.Id != context.User.Id)
                throw new RequestException(_textService.Error_CouldNotEditPost);

            // Edit the post
            var edittedPost = _postRepository.EditPost(post, request.Text, request.PostType, postFiles);

            // Create response
            var response = new EditPostResponseDto(edittedPost.ToDto(profile));

            return Task.FromResult(response);
        }
    }
}

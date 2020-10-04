using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class EditPostServerAction : ServerActionBase<EditPostRequestDto, EditPostResponseDto>
    {
        private static void LogAction(ILogger<EditPostServerAction> logger, PostModel post, EditPostRequestDto request)
        {
            logger.LogInformation("About to edit the post with ID: '{PostId}', Text '{OldText}', Category '{OldCategory}', Files count {OldFilesCount}, File names {OldFiles} to values Text '{NewText}', Category '{NewCategory}', Files count {NewFilesCount}, File names {NewFiles}",
                post.Id,
                post.Text,
                post.PostType ?? "null",
                post.PostFiles?.Count ?? 0,
                post.PostFiles?.Select(f => f.OriginalFullName).Aggregate(string.Empty, (a, b) => $"'{a}', '{b}'") ?? string.Empty,
                request.Text,
                request.PostType,
                request.PostFiles?.Count() ?? 0,
                request.PostFiles?.Select(f => f.OriginalFullName).Aggregate(string.Empty, (a, b) => $"'{a}', '{b}'") ?? string.Empty);
        }

        private readonly PostRepository _postRepository;
        private readonly TextService _textService;
        private readonly PostFileRepository _postFileRepository;
        private readonly ProfileRepository _profileRepository;
        private readonly ILogger<EditPostServerAction> _logger;
        private readonly PostFileService _postFileService;
        private readonly UniwikiContext _uniwikiContext;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public EditPostServerAction(IServiceProvider serviceProvider, PostRepository postRepository, TextService textService, PostFileRepository postFileRepository, ProfileRepository profileRepository, ILogger<EditPostServerAction> logger, PostFileService postFileService, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _postRepository = postRepository;
            _textService = textService;
            _postFileRepository = postFileRepository;
            _profileRepository = profileRepository;
            _logger = logger;
            _postFileService = postFileService;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<EditPostResponseDto> ExecuteAsync(EditPostRequestDto request, RequestContext context)
        {
            // Get post Id
            var postId = request.PostId;

            // Get post to edit
            var post = _uniwikiContext.Posts.Find(postId) ?? throw  new RequestException(_textService.CouldNotFindPost_Removing);

            // Check if the post belongs to the right user
            if (post.AuthorId != context.UserId!.Value)
            {
                throw new RequestException(_textService.Error_CouldNotEditPost);
            }

            // Log the change
            LogAction(_logger, post, request);

            // Update the names of the files
            _postFileService.UpdatePostFiles(context.UserId!.Value, post.Id, request.PostFiles.ToArray());

            // Edit the post
            var edittedPost = _postRepository.EditPost(post, request.Text, request.PostType);

            // Convert to DTO
            var postDto = edittedPost.ToPostViewModel(context.UserId!.Value).Single();

            // Create response
            var response = new EditPostResponseDto(postDto);

            return Task.FromResult(response);
        }
    }
}

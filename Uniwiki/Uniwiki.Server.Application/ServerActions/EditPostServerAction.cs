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
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared.Extensions;
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
                post.Files.Length,
                post.Files.Select(f => f.OriginalFullName).Aggregate(string.Empty, (a, b) => $"'{a}', '{b}'"),
                request.Text,
                request.PostType,
                request.PostFiles.Count(),
                request.PostFiles.Select(f => f.OriginalFullName).Aggregate(string.Empty, (a, b) => $"'{a}', '{b}'"));
        }

        private readonly IPostRepository _postRepository;
        private readonly TextService _textService;
        private readonly IPostFileRepository _postFileRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly ILogger<EditPostServerAction> _logger;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public EditPostServerAction(IServiceProvider serviceProvider, IPostRepository postRepository, TextService textService, IPostFileRepository postFileRepository, IProfileRepository profileRepository, ILogger<EditPostServerAction> logger) : base(serviceProvider)
        {
            _postRepository = postRepository;
            _textService = textService;
            _postFileRepository = postFileRepository;
            _profileRepository = profileRepository;
            _logger = logger;
        }

        protected override Task<EditPostResponseDto> ExecuteAsync(EditPostRequestDto request, RequestContext context)
        {
            // Get post Id
            var postId = request.PostId;

            // Get post to edit
            var post = _postRepository.FindById(postId);

            // Get user profile
            var profile = _profileRepository.FindById(context.User.Id);

            // Find all files from the request in DB
            var filesForSearch = request.PostFiles.Select(f => (f.Id, f.NameWithoutExtension));

            // Check if the post belongs to the right user
            if (post.Author.Id != context.User.Id)
                throw new RequestException(_textService.Error_CouldNotEditPost);

            // Log the change
            LogAction(_logger, post, request);

            // Update the names of the files
            var postFiles = _postFileRepository.FindPostFilesAndUpdateNames(filesForSearch, profile).ToArray();

            // Edit the post
            var edittedPost = _postRepository.EditPost(post, request.Text, request.PostType, postFiles);

            // Create response
            var response = new EditPostResponseDto(edittedPost.ToDto(profile));

            return Task.FromResult(response);
        }
    }
}

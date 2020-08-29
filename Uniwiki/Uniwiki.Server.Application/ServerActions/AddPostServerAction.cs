﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class AddPostServerAction : ServerActionBase<AddPostRequestDto, AddPostResponseDto>
    {
        private readonly CourseRepository _courseRepository;
        private readonly ProfileRepository _profileRepository;
        private readonly ITimeService _timeService;
        private readonly PostFileRepository _postFileRepository;
        private readonly PostRepository _postRepository;
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.PrimaryToken;

        public AddPostServerAction(IServiceProvider serviceProvider, CourseRepository courseRepository, ProfileRepository profileRepository, ITimeService timeService, PostFileRepository postFileRepository, PostRepository postRepository) : base(serviceProvider)
        {
            _courseRepository = courseRepository;
            _profileRepository = profileRepository;
            _timeService = timeService;
            _postFileRepository = postFileRepository;
            _postRepository = postRepository;
        }

        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        protected override Task<AddPostResponseDto> ExecuteAsync(AddPostRequestDto request, RequestContext context)
        {
            // Get files for the post
            var files = request.PostFiles.Select(f => (f.Id, f.NameWithoutExtension));

            // Add the post to the DB
            var post = _postRepository.AddPost(request.PostType, context.UserId!.Value, request.Text, request.CourseId, _timeService.Now);

            // TODO: ------------------ Move this to the PostFilesService and reuse in EditPostServerAction
            // Get post files from the DB
            var postFiles = _postFileRepository.FindPostFiles(files, context.UserId!.Value);

            // Get the new post files
            var newPostFiles = postFiles.Where(f => f.Post != null).ToArray();

            // Get all files with changed names
            var changedPostFiles = postFiles
                .Select(pf => (PostFile: pf, files.First(f => f.Id == pf.Id).NameWithoutExtension))
                .Where(p => p.NameWithoutExtension != p.PostFile.NameWithoutExtension);

            // Update the names for the postFiles
            _postFileRepository.UpdateNamesOfPostFiles(changedPostFiles);

            // Pair all the files to the new post
            var updatedPost = _postFileRepository.PairPostFilesWithPost(newPostFiles, post);
            // --------

            // Create DTO
            var postDto = updatedPost.ToDto(context.UserId).Single();

            return Task.FromResult(new AddPostResponseDto(postDto));
        }

    }
}

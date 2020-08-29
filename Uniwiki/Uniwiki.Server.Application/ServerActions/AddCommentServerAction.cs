﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class AddCommentServerAction : ServerActionBase<AddCommentRequestDto, AddCommentResponseDto>
    {
        private readonly ProfileRepository _profileRepository;
        private readonly PostRepository _postRepository;
        private readonly PostCommentRepository _postCommentRepository;
        private readonly ITimeService _timeService;
        private readonly TextService _textService;
        private readonly UniwikiContext _uniwikiContext;

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.PrimaryToken;

        public AddCommentServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, PostRepository postRepository, PostCommentRepository postCommentRepository, ITimeService timeService, TextService textService, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postRepository = postRepository;
            _postCommentRepository = postCommentRepository;
            _timeService = timeService;
            _textService = textService;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<AddCommentResponseDto> ExecuteAsync(AddCommentRequestDto request, RequestContext context)
        {
            // Create the comment
            _postCommentRepository.AddPostComment(context.UserId!.Value, request.PostId, request.CommentText, _timeService.Now);

            // Get the hole updated post
            var post = _uniwikiContext.Posts.ToDto(context.UserId).Single(p => p.Id == request.PostId);

            // Create response
            var response = new AddCommentResponseDto(post);

            return Task.FromResult(response);
        }
    }
}
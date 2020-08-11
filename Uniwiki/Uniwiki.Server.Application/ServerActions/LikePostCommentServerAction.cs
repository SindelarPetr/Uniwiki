﻿using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class LikePostCommentServerAction : ServerActionBase<LikePostCommentRequestDto, LikePostCommentResponseDto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPostCommentRepository _postCommentRepository;
        private readonly ITimeService _timeService;
        private readonly IPostCommentLikeRepository _postCommentLikeRepository;
        private readonly TextService _textService;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public LikePostCommentServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository, IPostCommentRepository postCommentRepository, ITimeService timeService, IPostCommentLikeRepository postCommentLikeRepository, TextService textService) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postCommentRepository = postCommentRepository;
            _timeService = timeService;
            _postCommentLikeRepository = postCommentLikeRepository;
            _textService = textService;
        }

        protected override Task<LikePostCommentResponseDto> ExecuteAsync(LikePostCommentRequestDto request, RequestContext context)
        {
            // Get profile
            var profile = _profileRepository.FindById(context.User.Id);

            // Get the comment to like
            var comment = _postCommentRepository.FindById(request.PostCommentId, _textService.Error_PostCommentNotFound);

            // Like the comment
            _postCommentLikeRepository.LikeComment(comment, profile, _timeService.Now);

            // Create response
            var response = new LikePostCommentResponseDto(comment.Post.ToDto(profile));

            return Task.FromResult(response);
        }
    }
}
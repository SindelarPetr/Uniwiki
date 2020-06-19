﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class GetCourseServerAction : ServerActionBase<GetCourseRequestDto, GetCourseResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly ICourseRepository _courseRepository;
        private readonly IPostRepository _postRepository;
        private readonly IPostTypeRepository _postTypeRepository;
        private readonly ICourseVisitRepository _courseVisitRepository;
        private readonly ITimeService _timeService;

        public GetCourseServerAction(IServiceProvider serviceProvider, ICourseRepository courseRepository, IPostRepository postRepository, IPostTypeRepository postTypeRepository, ICourseVisitRepository courseVisitRepository, ITimeService timeService) : base (serviceProvider)
        {
            _courseRepository = courseRepository;
            _postRepository = postRepository;
            _postTypeRepository = postTypeRepository;
            _courseVisitRepository = courseVisitRepository;
            _timeService = timeService;
        }

        protected override Task<GetCourseResponseDto> ExecuteAsync(GetCourseRequestDto request, RequestContext context)
        {
            // Get profile
            var profile = context.User;

            // Get course for the request
            var course = _courseRepository.GetCourse(request.UniversityUrl, request.StudyGroupUrl, request.CourseUrl);

            // Get post type counts
            var filterPostTypes = _postTypeRepository.GetFilterPostTypes(course).Select(pair => new FilterPostTypeDto(pair.Item1, pair.Item2)).ToArray();

            // Get posts for the course
            var posts = request.ShowAll 
                ? _postRepository.FetchPosts(course, null, request.PostsToFetch).ToArray() 
                : _postRepository.FetchPosts(course, request.PostType, null,  request.PostsToFetch).ToArray();

            // Check if can fetch more posts
            var canFetchMore = request.ShowAll
                ? _postRepository.CanFetchMore(course, posts.LastOrDefault())
                : _postRepository.CanFetchMore(course, request.PostType, posts.LastOrDefault());

            var postDtos = posts.Select(p => p.ToDto(profile)).ToArray();

            // Post types
            var postTypesForNewPost = _postTypeRepository.GetPostTypesForNewPost(course).ToArray();

            // Convert course to Dto
            var courseDto = course.ToDto();

            // Set the course as recent
            if (context.IsAuthenticated) // Equivalent to context.IsAuthenticated
                _courseVisitRepository.AddCourseVisit(course, profile, _timeService.Now);

            // Create response
            var response = new GetCourseResponseDto(request.PostType, 
                    postDtos,
                    filterPostTypes,
                    courseDto,
                    postTypesForNewPost,
                    canFetchMore);

            return Task.FromResult(response);
        }
    }
}

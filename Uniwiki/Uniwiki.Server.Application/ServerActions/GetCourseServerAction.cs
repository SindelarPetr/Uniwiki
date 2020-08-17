using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse;

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
        private readonly IPostCategoryService _postCategoryService;

        public GetCourseServerAction(IServiceProvider serviceProvider, ICourseRepository courseRepository, IPostRepository postRepository, ICourseVisitRepository courseVisitRepository, ITimeService timeService, IPostCategoryService postCategoryService) : base (serviceProvider)
        {
            _courseRepository = courseRepository;
            _postRepository = postRepository;
            _courseVisitRepository = courseVisitRepository;
            _timeService = timeService;
            _postCategoryService = postCategoryService;
        }

        protected override Task<GetCourseResponseDto> ExecuteAsync(GetCourseRequestDto request, RequestContext context)
        {
            // Get profile
            var profile = context.User;

            // Get course for the request
            var course = _courseRepository.GetCourseFromUrl(request.UniversityUrl, request.StudyGroupUrl, request.CourseUrl);

            // Get category counts for filtering
            var filterPostTypes = _postCategoryService.GetFilterCategories(course).Select(pair => new FilterPostTypeDto(pair.Item1, pair.Item2)).ToArray();

            // Get posts for the course
            var posts = request.ShowAll 
                ? _postRepository.FetchPosts(course, null, request.PostsToFetch).ToArray() 
                : _postRepository.FetchPosts(course, request.PostType, null,  request.PostsToFetch).ToArray();

            // Check if can fetch more posts
            var canFetchMore = request.ShowAll
                ? _postRepository.CanFetchMore(course, posts.LastOrDefault())
                : _postRepository.CanFetchMore(course, request.PostType, posts.LastOrDefault());

            var postDtos = posts.Select(p => p.ToDto(profile)).ToArray();

            // Post types // TODO: FIX
            var postTypesForNewPost = _postCategoryService.GetCategoriesForNewPost(course); // _postTypeRepository.GetPostTypesForNewPost(course).ToArray();

            // Convert course to Dto
            var courseDto = course.ToDto();

            // Set the course as recent
            if (context.IsAuthenticated) // Equivalent to context.IsAuthenticated
            {
                _courseVisitRepository.AddCourseVisit(course, profile, _timeService.Now);
            }

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

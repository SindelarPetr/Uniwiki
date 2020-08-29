using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Configuration;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class GetCourseServerAction : ServerActionBase<GetCourseRequestDto, GetCourseResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly CourseRepository _courseRepository;
        private readonly PostRepository _postRepository;
        private readonly CourseVisitRepository _courseVisitRepository;
        private readonly ITimeService _timeService;
        private readonly IPostCategoryService _postCategoryService;
        private readonly UniwikiContext _uniwikiContext;
        private readonly TextService _textService;
        private readonly UniwikiConfiguration _uniwikiConfiguration;
        private readonly ILanguageService _languageService;
        private readonly FetchPostsService _fetchPostsService;

        public GetCourseServerAction(IServiceProvider serviceProvider, CourseRepository courseRepository, PostRepository postRepository, CourseVisitRepository courseVisitRepository, ITimeService timeService, IPostCategoryService postCategoryService, UniwikiContext uniwikiContext, TextService textService, UniwikiConfiguration uniwikiConfiguration, ILanguageService languageService, FetchPostsService fetchPostsService) : base(serviceProvider)
        {
            _courseRepository = courseRepository;
            _postRepository = postRepository;
            _courseVisitRepository = courseVisitRepository;
            _timeService = timeService;
            _postCategoryService = postCategoryService;
            _uniwikiContext = uniwikiContext;
            _textService = textService;
            _uniwikiConfiguration = uniwikiConfiguration;
            _languageService = languageService;
            _fetchPostsService = fetchPostsService;
        }

        protected override Task<GetCourseResponseDto> ExecuteAsync(GetCourseRequestDto request, RequestContext context)
        {
            // Normalize the urls
            var neutralizedCourseUrl = request.CourseUrl.Neutralize();
            var neutralizedStudyGroupUrl = request.StudyGroupUrl.Neutralize();
            var neutralizedUniversityUrl = request.UniversityUrl.Neutralize();

            // Find the course according to the URL
            var course = _uniwikiContext
                .Courses
                .Include(c => c.StudyGroup)
                .ThenInclude(g => g.University)
                .SingleOrDefault(c => c.Url == neutralizedCourseUrl 
                             && c.StudyGroupUrl == neutralizedStudyGroupUrl 
                             && c.UniversityUrl == neutralizedUniversityUrl);
            
            if(course == null)
            {
                throw new RequestException(_textService.Error_CourseNotFound);
            }

            // Get categories and their counts for filtering
            var filterPostTypes = _uniwikiContext
                .Posts
                .Where(p => p.CourseId == course.Id)
                .GroupBy(p => p.PostType)
                .Select(g => new FilterPostTypeDto(g.Key, g.Count())) // TODO: This might cause problems
                .ToArray();

            var (postViewModels, canFetchMore) = _fetchPostsService.FetchPosts(!request.ShowAll, course.Id, request.PostType,
                request.LastPostNumber, request.PostsToFetch, context.UserId);

            // Get categories for a new post
            var postTypesForNewPost = _uniwikiContext
                .Posts
                .Where(p => p.CourseId == course.Id && p.PostType != null)
                .Select(p => p.PostType!)
                .Concat(_languageService.GetTranslation(
                    _uniwikiConfiguration.Defaults.DefaultPostCategoriesCz, _uniwikiConfiguration.Defaults.DefaultPostCategoriesEn))
                .Distinct()
                .ToArray();

            //_postCategoryService.GetCategoriesForNewPost(course.Id).ToArray(); // _postTypeRepository.GetPostTypesForNewPost(course).ToArray();

            // Set the course as recent
            if (context.IsAuthenticated) // Equivalent to context.IsAuthenticated
            {
                _courseVisitRepository.AddCourseVisit(course.Id, context.UserId!.Value, _timeService.Now);
            }

            var posts = postViewModels.ToArray();

            // Create response
            var response = new GetCourseResponseDto(request.PostType,
                    posts,
                    filterPostTypes,
                    course.Id,
                    postTypesForNewPost,
                    canFetchMore,
                    new RecentCourseDto(
                        course.LongName,
                        course.Code,
                        course.StudyGroupUrl, 
                        course.StudyGroup.LongName,
                        course.UniversityUrl, 
                        course.StudyGroup.University.ShortName, 
                        course.Id, 
                        course.Url
                        ),
                    course.LongName,
                    course.StudyGroup.University.ShortName + " - " + course.StudyGroup.LongName);

            return Task.FromResult(response);
        }
    }
}

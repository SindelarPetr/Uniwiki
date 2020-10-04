
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Appliaction.ServerActions;
using Shared;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Shared.Configuration;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Server.Application.ServerActions
{
    internal class GetCourseServerAction : ServerActionBase<GetCourseRequestDto, GetCourseResponseDto>
    {
        private readonly static string[] _defaultPostCategoriesCz = {
        "Domácí úkol",
        "Test v semesteru",
        "Zkouška",
        "Studijní materiál"
        };

        private readonly static string[] _defaultPostCategoriesEn = {
            "Homework",
            "Test in semester",
            "Final exam",
            "Study material"
        };

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly CourseVisitRepository _courseVisitRepository;
        private readonly ITimeService _timeService;
        private readonly UniwikiContext _uniwikiContext;
        private readonly TextService _textService;
        private readonly UniwikiConfiguration _uniwikiConfiguration;
        private readonly FetchPostsService _fetchPostsService;

        public GetCourseServerAction(IServiceProvider serviceProvider, CourseVisitRepository courseVisitRepository, ITimeService timeService, UniwikiContext uniwikiContext, TextService textService, UniwikiConfiguration uniwikiConfiguration, FetchPostsService fetchPostsService) : base(serviceProvider)
        {
            _courseVisitRepository = courseVisitRepository;
            _timeService = timeService;
            _uniwikiContext = uniwikiContext;
            _textService = textService;
            _uniwikiConfiguration = uniwikiConfiguration;
            _fetchPostsService = fetchPostsService;
        }

        protected override Task<GetCourseResponseDto> ExecuteAsync(GetCourseRequestDto request, RequestContext context)
        {
            // Find the course according to the URL
            var course = _uniwikiContext
                .Courses
                .Include(c => c.StudyGroup)
                .ThenInclude(g => g.University)
                .Where(c => c.Url == request.CourseUrl
                            && c.StudyGroupUrl == request.StudyGroupUrl
                            && c.UniversityUrl == request.UniversityUrl)
                .FirstOrDefault() ?? throw new RequestException(_textService.Error_CourseNotFound);

            // Get categories and their counts for filtering
            var filterPostTypes = _uniwikiContext
                .Posts
                .Where(p => p.CourseId == course.Id)
                .GroupBy(p => p.PostType)
                .Select(g => new FilterPostTypeDto(g.Key, g.Count()))
                .ToArray();

            var (postViewModels, canFetchMore) 
                = _fetchPostsService.FetchPosts(!request.ShowAll, course.Id, request.PostType, request.LastPostCreationTime, request.PostsToFetch, context.UserId);

            var posts = postViewModels.ToArray();

            var defaultPostTypes = course.StudyGroup.PrimaryLanguage == Language.Czech ?
                _defaultPostCategoriesCz :
                _defaultPostCategoriesEn;

            // Get categories for a new post
            var postTypesForNewPost = _uniwikiContext
                .Posts
                .Where(p => p.CourseId == course.Id && p.PostType != null)
                .Select(p => p.PostType!)
                .Distinct()
                .AsEnumerable()
                .Concat(defaultPostTypes)
                .Distinct()
                .ToArray();

            // Set the course as recent
            if (context.IsAuthenticated) // Equivalent to context.IsAuthenticated
            {
                _courseVisitRepository.AddCourseVisit(course.Id, context.UserId!.Value, _timeService.Now);
            }

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

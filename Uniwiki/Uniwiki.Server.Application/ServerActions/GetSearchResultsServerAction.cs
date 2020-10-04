using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Server.Appliaction.ServerActions;
using Shared.Services;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class GetSearchResultsServerAction : ServerActionBase<GetSearchResultsRequestDto, GetSearchResultsResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly StudyGroupRepository _studyGroupRepository;
        private readonly CourseRepository _courseRepository;
        private readonly StringStandardizationService _stringStandardizationService;
        private readonly CourseVisitRepository _courseVisitRepository;
        private readonly ILogger<GetSearchResultsServerAction> _logger;
        private readonly UniwikiContext _uniwikiContext;

        public GetSearchResultsServerAction(IServiceProvider serviceProvider, StudyGroupRepository studyGroupRepository, CourseRepository courseRepository, StringStandardizationService stringStandardizationService, CourseVisitRepository courseVisitRepository, ILogger<GetSearchResultsServerAction> logger, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _studyGroupRepository = studyGroupRepository;
            _courseRepository = courseRepository;
            _stringStandardizationService = stringStandardizationService;
            _courseVisitRepository = courseVisitRepository;
            _logger = logger;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<GetSearchResultsResponseDto> ExecuteAsync(GetSearchResultsRequestDto request, RequestContext context)
        {
            // Get search text
            var searchText = _stringStandardizationService.StandardizeSearchText(request.SearchedText);

            // Log the search text
            _logger.LogInformation("Searching for text: '{Text}', standardized: '{StandardizedText}'", request.SearchedText, searchText);

            // Which should be found
            FoundCourseDto[] courseDtos;

            // Recent courses for the user
            RecentCourseDto[] recentCourseDtos = new RecentCourseDto[0];

            // Find the recent courses if the user is authenticated and did not type any text
            if (context.IsAuthenticated && string.IsNullOrWhiteSpace(searchText))
            {
                // TODO: Check efficiency of this - how many calls are made?
                // TODO: Fix
                recentCourseDtos =
                    _uniwikiContext
                    .CourseVisits
                    .Include(v => v.Course)
                    .Where(v => v.ProfileId == context.UserId!.Value)
                    .OrderByDescending(v => v.VisitDateTime)
                    .Select(v => v.Course) // This might cause a problem
                    .Distinct()
                    .ToRecentCourseDto()
                    .ToArray();
            }

            // Check if the user selected a study group
            if (request.StudyGroupId != null)
            {
                // Search just amongst the selected study group
                var coursesFromStudyGroup = _uniwikiContext
                    .Courses
                    .Include(c => c.StudyGroup)
                    .ThenInclude(g => g.University)
                    .Where(c => c.StudyGroupId.Equals(request.StudyGroupId));

                // find the courses
                var courses = FindCourses(coursesFromStudyGroup, searchText);

                courseDtos = courses.ToFoundCourses().ToArray();
            }
            else // User did not select a study group
            {
                // If the user did not type any text
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // Return empty results
                    courseDtos = new FoundCourseDto[0];
                }
                else // The user typed some text
                {
                    // Find the courses
                    courseDtos = FindCourses(_uniwikiContext.Courses, searchText).ToFoundCourses().ToArray();
                }
            }

            // Create the response
            var response = new GetSearchResultsResponseDto(recentCourseDtos, courseDtos);

            return Task.FromResult(response);
        }

        private IQueryable<CourseModel> FindCourses(IQueryable<CourseModel> courses, string searchText)
        => string.IsNullOrWhiteSpace(searchText)
            ? courses
            : courses.Where(c => c.FullNameStandardized.Contains(searchText) || c.Code.Contains(searchText));
    }
}

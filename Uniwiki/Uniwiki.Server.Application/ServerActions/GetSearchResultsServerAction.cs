using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class GetSearchResultsServerAction : ServerActionBase<GetSearchResultsRequestDto, GetSearchResultsResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly IStudyGroupRepository _studyGroupRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStringStandardizationService _stringStandardizationService;
        private readonly ICourseVisitRepository _courseVisitRepository;
        private readonly ILogger<GetSearchResultsServerAction> _logger;

        public GetSearchResultsServerAction(IServiceProvider serviceProvider, IStudyGroupRepository studyGroupRepository, ICourseRepository courseRepository, IStringStandardizationService stringStandardizationService, ICourseVisitRepository courseVisitRepository, ILogger<GetSearchResultsServerAction> logger) : base(serviceProvider)
        {
            _studyGroupRepository = studyGroupRepository;
            _courseRepository = courseRepository;
            _stringStandardizationService = stringStandardizationService;
            _courseVisitRepository = courseVisitRepository;
            _logger = logger;
        }

        protected override Task<GetSearchResultsResponseDto> ExecuteAsync(GetSearchResultsRequestDto request, RequestContext context)
        {
            // Get search text
            var searchText = _stringStandardizationService.StandardizeSearchText(request.SearchedText);

            // Log the search text
            _logger.LogInformation("Searching for text: '{Text}', standardized: '{StandardizedText}'", request.SearchedText, searchText);

            // Which should be found
            CourseDto[] courseDtos;

            // Recent courses for the user
            CourseDto[] recentCourseDtos = new CourseDto[0];

            // Find the recent courses if the user is authenticated and did not type any text
            if (context.IsAuthenticated && string.IsNullOrWhiteSpace(searchText))
            {
                recentCourseDtos = _courseVisitRepository.GetRecentCourses(context.User).Select(c => c.ToDto()).ToArray();
            }

            // Check if the user wants to filter by study group
            var studyGroup = request.StudyGroupId != null
                ? _studyGroupRepository.FindById(request.StudyGroupId.Value)
                : null;

            // User selected a study group
            if (studyGroup != null)
            {
                courseDtos = _courseRepository.SearchCoursesFromStudyGroup(searchText, studyGroup).Select(c => c.ToDto()).ToArray();
            }
            else // User did not select a study group
            {
                // If the user did not type any text
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // Return empty results
                    courseDtos = new CourseDto[0];
                }
                else // The user typed some text
                {
                    // Find the courses
                    courseDtos = _courseRepository.SearchCourses(searchText).Select(c => c.ToDto()).ToArray();
                }
            }

            // Create the response
            var response = new GetSearchResultsResponseDto(recentCourseDtos, courseDtos);

            return Task.FromResult(response);
        }
    }
}

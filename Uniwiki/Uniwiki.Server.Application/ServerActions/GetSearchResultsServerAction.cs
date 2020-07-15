using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class GetSearchResultsServerAction : ServerActionBase<GetSearchResultsRequestDto, GetSearchResultsResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly IUniversityRepository _universityRepository;
        private readonly IStudyGroupRepository _studyGroupRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStringStandardizationService _stringStandardizationService;
        private readonly IProfileRepository _profileRepository;
        private readonly ICourseVisitRepository _courseVisitRepository;
        private readonly ILogger<GetSearchResultsServerAction> _logger;

        public GetSearchResultsServerAction(IServiceProvider serviceProvider, IUniversityRepository universityRepository, IStudyGroupRepository studyGroupRepository, ICourseRepository courseRepository, IStringStandardizationService stringStandardizationService, IProfileRepository profileRepository, ICourseVisitRepository courseVisitRepository, ILogger<GetSearchResultsServerAction> logger) : base(serviceProvider)
        {
            _universityRepository = universityRepository;
            _studyGroupRepository = studyGroupRepository;
            _courseRepository = courseRepository;
            _stringStandardizationService = stringStandardizationService;
            _profileRepository = profileRepository;
            _courseVisitRepository = courseVisitRepository;
            _logger = logger;
        }

        protected override Task<GetSearchResultsResponseDto> ExecuteAsync(GetSearchResultsRequestDto request, RequestContext context)
        {
            // Get search text
            var searchText = _stringStandardizationService.StandardizeSearchText(request.SearchedText);

            // Log the search text
            _logger.LogInformation("Searching for text: '{Text}', standardized: '{StandardizedText}'", request.SearchedText, searchText);

            UniversityDto[] universityDtos;
            StudyGroupDto[] studyGroupDtos;
            CourseDto[] courseDtos;
            CourseDto[] recentCourseDtos = new CourseDto[0];


            var studyGroup = request.StudyGroupId != null
                ? _studyGroupRepository.FindById(request.StudyGroupId.Value)
                : null;

            var university = request.UniversityId != null 
                ? _universityRepository.FindById(request.UniversityId.Value) 
                : null;

            // Show recent courses to authenticated users, who did not type any search text
            if (context.IsAuthenticated && string.IsNullOrWhiteSpace(searchText))
            {
                var profile = _profileRepository.FindById(context.User.Id);
                recentCourseDtos = _courseVisitRepository.GetRecentCourses(studyGroup, profile).Select(c => c.ToDto()).ToArray();
            }

            if (studyGroup != null)
            {
                universityDtos = new UniversityDto[0];
                studyGroupDtos = new StudyGroupDto[0];
                courseDtos = _courseRepository.SearchCoursesFromStudyGroup(searchText, studyGroup).Select(c => c.ToDto()).ToArray();
            }
            else
            {
                if (university == null)
                {
                    if (string.IsNullOrWhiteSpace(searchText))
                    {
                        universityDtos = _universityRepository.GetUniversities().Select(u => u.ToDto()).ToArray();
                        studyGroupDtos = new StudyGroupDto[0];
                        courseDtos = new CourseDto[0];
                    }
                    else
                    {
                        universityDtos = _universityRepository.SearchUniversities(searchText).Select(u => u.ToDto())
                            .ToArray();
                        studyGroupDtos = _studyGroupRepository.SearchStudyGroups(searchText).Select(g => g.ToDto())
                            .ToArray();
                        courseDtos = _courseRepository.SearchCourses(searchText).Select(c => c.ToDto()).ToArray();
                    }
                }
                else
                {
                    universityDtos = new UniversityDto[0];

                    if (String.IsNullOrWhiteSpace(searchText))
                    {
                        studyGroupDtos = _studyGroupRepository.SearchStudyGroupsOfUniversity(searchText, university).Select(g => g.ToDto()).ToArray();
                        courseDtos = new CourseDto[0];
                    }
                    else
                    {
                        studyGroupDtos = _studyGroupRepository.SearchStudyGroupsOfUniversity(searchText, university)
                            .Select(g => g.ToDto()).ToArray();
                        courseDtos = _courseRepository.SearchCoursesFromUniversity(searchText, university)
                            .Select(c => c.ToDto()).ToArray();
                    }
                }
            }

            var response = new GetSearchResultsResponseDto(recentCourseDtos, universityDtos, studyGroupDtos, courseDtos);

            return Task.FromResult(response);
        }
    }
}

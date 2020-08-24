using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class AddCourseServerAction : ServerActionBase<AddCourseRequestDto, AddCourseResponseDto>
    {
        private readonly CourseRepository _courseRepository;
        private readonly StudyGroupRepository _studyGroupRepository;
        private readonly ProfileRepository _profileRepository;
        private readonly IStringStandardizationService _stringStandardizationService;
        private readonly TextService _textService;
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.PrimaryToken;

        public AddCourseServerAction(IServiceProvider serviceProvider, CourseRepository courseRepository, StudyGroupRepository studyGroupRepository, ProfileRepository profileRepository, IStringStandardizationService stringStandardizationService, TextService textService) : base(serviceProvider)
        {
            _courseRepository = courseRepository;
            _studyGroupRepository = studyGroupRepository;
            _profileRepository = profileRepository;
            _stringStandardizationService = stringStandardizationService;
            _textService = textService;
        }

        protected override Task<AddCourseResponseDto> ExecuteAsync(AddCourseRequestDto request, RequestContext context)
        {
            // Get course information
            var name = request.CourseName;
            var code = request.CourseCode;
            var studyGroupId = request.StudyGroupId;

            // Get study group with university
            var faculty = _studyGroupRepository.GetStudyGroupWithUniversity(studyGroupId);

            // Check if the course name is unique
            if(!_courseRepository.IsNameUnique(faculty, name))
                throw new RequestException(_textService.Error_CourseNameTaken(name));

            // Create url for the course
            var url = _stringStandardizationService.CreateUrl(name, u => _courseRepository.IsUrlUnique(faculty, u));

            // Create the course
            var course = _courseRepository.AddCourse(code, name, context.User!, faculty, faculty.University.Url, url);

            course = _courseRepository.GetCourseWithStudyGroupAndUniversity(course.Id);

            // Create course DTO
            var courseDto = course.ToDto();

            return Task.FromResult(new AddCourseResponseDto(courseDto));
        }

    }
}
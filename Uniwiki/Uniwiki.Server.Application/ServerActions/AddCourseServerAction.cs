using System;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class AddCourseServerAction : ServerActionBase<AddCourseRequestDto, AddCourseResponseDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudyGroupRepository _studyGroupRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IStringStandardizationService _stringStandardizationService;
        private readonly TextService _textService;
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.PrimaryToken;

        public AddCourseServerAction(IServiceProvider serviceProvider, ICourseRepository courseRepository, IStudyGroupRepository studyGroupRepository, IProfileRepository profileRepository, IStringStandardizationService stringStandardizationService, TextService textService) : base(serviceProvider)
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

            // Get author
            var author = _profileRepository.FindById(context.User.Id);

            // Get study group
            var faculty = _studyGroupRepository.FindById(studyGroupId);

            // Check if the course name is unique
            if(!_courseRepository.IsNameUnique(faculty, name))
                throw new RequestException(_textService.Error_CourseNameTaken(name));

            // Create url for the course
            var url = _stringStandardizationService.CreateUrl(name, u => _courseRepository.IsUrlUnique(faculty, u));

            // Create the course
            var course = new CourseModel(Guid.NewGuid(), code, name, faculty, author, url, false);
            _courseRepository.Add(course);

            // Create course DTO
            var courseDto = course.ToDto();

            return Task.FromResult(new AddCourseResponseDto(courseDto));
        }

    }
}
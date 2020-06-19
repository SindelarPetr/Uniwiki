using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class AddStudyGroupResponseDto : ResponseBase
    {
        public StudyGroupDto StudyGroupDto { get; set; }

        public AddStudyGroupResponseDto(StudyGroupDto studyGroupDto)
        {
            StudyGroupDto = studyGroupDto;
        }
    }
}
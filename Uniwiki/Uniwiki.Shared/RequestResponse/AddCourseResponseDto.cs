using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class AddCourseResponseDto : ResponseBase
    {
        public CourseDto CourseDto { get; set; }


        public AddCourseResponseDto(CourseDto courseDto)
        {
            CourseDto = courseDto;
        }
    }
}
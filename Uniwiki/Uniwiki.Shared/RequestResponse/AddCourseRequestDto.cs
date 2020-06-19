using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse
{
    public class AddCourseRequestDto : RequestBase<AddCourseResponseDto>
    {
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public Guid StudyGroupId { get; set; }

        public AddCourseRequestDto(string courseName, string courseCode, Guid studyGroupId)
        {
            CourseName = courseName;
            CourseCode = courseCode;
            StudyGroupId = studyGroupId;
        }
    }
}
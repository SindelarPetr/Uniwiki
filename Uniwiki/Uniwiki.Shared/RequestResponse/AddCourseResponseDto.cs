using System;
using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class AddCourseResponseDto : ResponseBase
    {
        public AddCourseResponseDto(Guid courseId, string universityUrl, string studyGroupUrl, string courseUrl)
        {
            CourseId = courseId;
            UniversityUrl = universityUrl;
            StudyGroupUrl = studyGroupUrl;
            CourseUrl = courseUrl;
        }

        public Guid CourseId { get; }
        public string UniversityUrl { get; }
        public string StudyGroupUrl { get; }
        public string CourseUrl { get; }
    }
}
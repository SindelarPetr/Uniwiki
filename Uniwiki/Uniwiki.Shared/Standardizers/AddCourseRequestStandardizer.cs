﻿using Shared.Extensions;
using Shared.Services.Abstractions;
using Shared.Standardizers;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Shared.Standardizers
{
    internal class AddCourseRequestStandardizer : IStandardizer<AddCourseRequestDto>
    {
        private readonly IStringStandardizationService _stringStandardizationService;

        public AddCourseRequestStandardizer(IStringStandardizationService stringStandardizationService)
        {
            _stringStandardizationService = stringStandardizationService;
        }

        public AddCourseRequestDto Standardize(AddCourseRequestDto model) =>
            new AddCourseRequestDto(
                _stringStandardizationService.OptimizeWhiteSpaces(model.CourseName).FirstCharToUpper(),
                _stringStandardizationService.OptimizeWhiteSpaces(model.CourseCode),
                model.StudyGroupId);
    }
}

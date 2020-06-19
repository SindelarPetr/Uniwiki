using FluentValidation;
using Shared.Standardizers;
using Shared.Validators;
using Uniwiki.Shared.Extensions;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.Services;

namespace Uniwiki.Shared.Validators
{
    public class AddCourseRequestValidator : StandardizerValidator<AddCourseRequestDto>
    {
        public AddCourseRequestValidator(TextServiceBase textServiceBase, IStandardizer<AddCourseRequestDto> standardizer) : base(standardizer)
        {
            RuleFor(f => f.CourseCode)
                .MinMaxLengthWithMessages(textServiceBase, Constants.Validations.CourseCodeMinLength, Constants.Validations.CourseCodeMaxLength);

            RuleFor(f => f.StudyGroupId)
                .NotEmpty().WithMessage(textServiceBase.Validation_YouMustSelectStudyGroup);

            RuleFor(f => f.CourseName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .MinMaxLengthWithMessages(textServiceBase, Constants.Validations.CourseNameMinLength,
                    Constants.Validations.CourseNameMaxLength);
        }
    }
}

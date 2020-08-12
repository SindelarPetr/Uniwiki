using FluentValidation;
using Shared.Standardizers;
using Shared.Validators;
using Uniwiki.Shared.Extensions;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.Services;

namespace Uniwiki.Shared.Validators
{

    internal class AddStudyGroupRequestValidator : StandardizerValidator<AddStudyGroupRequestDto>
    {
        public AddStudyGroupRequestValidator(TextServiceShared textServiceBase, IStandardizer<AddStudyGroupRequestDto> standardizer) : base(standardizer)
        {
            RuleFor(f => f.StudyGroupName)
                .MinMaxLengthWithMessages(textServiceBase, Constants.Validations.StudyGroupNameMinLength, Constants.Validations.StudyGroupNameMaxLength);

            RuleFor(f => f.StudyGroupShortcut)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .MinMaxLengthWithMessages(textServiceBase, Constants.Validations.StudyGroupShortcutMinLength,
                    Constants.Validations.StudyGroupShortcutMaxLength);

            RuleFor(f => f.UniversityId)
                .NotEmpty().WithMessage(textServiceBase.Validation_NonEmpty);

            RuleFor(f => f.PrimaryLanguage)
                .IsInEnum()
                .WithMessage(textServiceBase.Validation_InvalidValue);
        }
    }
}

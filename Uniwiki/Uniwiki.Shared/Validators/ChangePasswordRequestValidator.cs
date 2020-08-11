using FluentValidation;
using Shared.Standardizers;
using Shared.Validators;
using Uniwiki.Shared.RequestResponse.Authentication;
using Uniwiki.Shared.Services;

namespace Uniwiki.Shared.Validators
{
    internal class ChangePasswordRequestValidator : StandardizerValidator<ChangePasswordRequestDto>
    {
        public ChangePasswordRequestValidator(TextServiceShared textServiceBase, IStandardizer<ChangePasswordRequestDto> standardizer) : base(standardizer)
        {
            RuleFor(f => f.OldPassword).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(textServiceBase.Validation_TypeYourOldPassword);

            RuleFor(f => f.NewPassword).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(textServiceBase.Validation_TypeYourNewPassword)
                .MinimumLength(1).WithMessage(textServiceBase.Validation_TypePasswordMatchingRequirements)
                .NotEqual(f2 => f2.OldPassword).WithMessage(textServiceBase.Validation_OldAndNewPasswordsCantMatch);

            RuleFor(f => f.NewPasswordAgain).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(textServiceBase.Validation_TypeYourNewPassword)
                .Equal(f2 => f2.NewPassword).WithMessage(textServiceBase.Validation_PasswordIsNotRepeatedCorrectly);
        }
    }
}
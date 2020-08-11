using FluentValidation;
using Shared.Standardizers;
using Shared.Validators;
using Uniwiki.Shared.RequestResponse.Authentication;
using Uniwiki.Shared.Services;

namespace Uniwiki.Shared.Validators
{
    internal class CreateNewPasswordRequestValidator : StandardizerValidator<CreateNewPasswordRequestDto>
    {
        public CreateNewPasswordRequestValidator(TextServiceShared textService, IStandardizer<CreateNewPasswordRequestDto> standardizer) : base(standardizer)
        {
            RuleFor(f => f.NewPassword).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(textService.Validation_TypeYourNewPassword);

            RuleFor(f => f.NewPasswordAgain).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(textService.Validation_TypeYourNewPassword)
                .Equal(f => f.NewPassword).WithMessage(textService.Validation_PasswordIsNotRepeatedCorrectly);
        }
    }
}
using FluentValidation;
using Shared.Standardizers;
using Shared.Validators;
using System;
using Uniwiki.Shared.Extensions;
using Uniwiki.Shared.RequestResponse.Authentication;
using Uniwiki.Shared.Services;

namespace Uniwiki.Shared.Validators
{
    internal class RegisterRequestValidator : StandardizerValidator<RegisterRequestDto>
    {
        public RegisterRequestValidator(TextServiceShared textService, IStandardizer<RegisterRequestDto> standardizer, Constants constants) : base(standardizer)
        {
            RuleFor(f => f.NameAndSurname).Cascade(CascadeMode.StopOnFirstFailure)
                .Must(n => n.Contains(" ")).WithMessage(textService.Validation_FillNameAndSurname)
                .MinMaxLengthWithMessages(textService, Constants.Validations.UserNameAndSurnameMinLength, Constants.Validations.UserNameAndSurnameMaxLength);

            RuleFor(f => f.Email).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(textService.Validation_TypeYourEmail)
                .EmailAddress().WithMessage(textService.Validation_TypeValidEmailAddress);

            RuleFor(f => f.Password).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(textService.Validation_TypeYourPassword)
                .MinMaxLengthWithMessages(textService, constants.PasswordMinLength, Constants.PasswordMaxLength);
            //.MinimumLength(1).WithMessage(textService.Validation_TypePasswordMatchingRequirements);

            RuleFor(f => f.AgreeToTermsOfUse)
                .Equal(true)
                .WithMessage(textService.Validation_YouNeedToAgreeToTermsOfUse);

            RuleFor(f => f.PasswordAgain).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(textService.Validation_TypeYourNewPassword)
                .Equal(f => f.Password).WithMessage(textService.Validation_PasswordIsNotRepeatedCorrectly);
        }
    }
}
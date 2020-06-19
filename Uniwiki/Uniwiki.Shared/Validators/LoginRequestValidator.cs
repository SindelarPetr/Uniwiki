using FluentValidation;
using Shared.Standardizers;
using Shared.Validators;
using Uniwiki.Shared.RequestResponse.Authentication;
using Uniwiki.Shared.Services;

namespace Uniwiki.Shared.Validators
{
    internal class LoginRequestValidator : StandardizerValidator<LoginRequestDto>
    {
        public LoginRequestValidator(TextServiceBase textService, IStandardizer<LoginRequestDto> standardizer) : base(standardizer)
        {
            RuleFor(f => f.Email).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(textService.Validation_TypeValidEmailAddress)
                .EmailAddress().WithMessage(textService.Validation_TypeValidEmailAddress);

            RuleFor(f => f.Password).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(textService.Validation_TypeYourPassword)
                .Length(1, 30).WithMessage(textService.Validation_TypePasswordMatchingRequirements);
        }
    }
}
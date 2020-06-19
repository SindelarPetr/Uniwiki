using FluentValidation;
using Shared.Standardizers;
using Shared.Validators;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.Services;

namespace Uniwiki.Shared.Validators
{
    internal class IsEmailAvailableRequestValidator : StandardizerValidator<IsEmailAvailableRequestDto>
    {
        public IsEmailAvailableRequestValidator(TextServiceBase textService, IStandardizer<IsEmailAvailableRequestDto> standardizer) : base(standardizer)
        {
            RuleFor(f => f.Email).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(textService.Validation_TypeYourEmail)
                .EmailAddress().WithMessage(textService.Validation_TypeValidEmailAddress);
        }
    }
}
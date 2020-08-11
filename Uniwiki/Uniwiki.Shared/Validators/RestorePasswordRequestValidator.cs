using FluentValidation;
using Shared.Standardizers;
using Shared.Validators;
using Uniwiki.Shared.RequestResponse.Authentication;
using Uniwiki.Shared.Services;

namespace Uniwiki.Shared.Validators
{
    internal class RestorePasswordRequestValidator : StandardizerValidator<RestorePasswordRequestDto>
    {
        public RestorePasswordRequestValidator(TextServiceShared textService, IStandardizer<RestorePasswordRequestDto> standardizer)
            :base(standardizer)
        {
            RuleFor(f => f.Email).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(textService.Validation_TypeYourEmail)
                .EmailAddress().WithMessage(textService.Validation_TypeValidEmailAddress);
        }
    }
}
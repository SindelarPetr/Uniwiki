using FluentValidation;
using Uniwiki.Shared.Extensions;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.Services;

namespace Uniwiki.Shared.Validators
{

    internal class EditPostRequestValidator : AbstractValidator<EditPostRequestDto>
    {
        public EditPostRequestValidator(TextServiceShared textServiceBase)
        {
            RuleFor(f => f.Text).Cascade(CascadeMode.StopOnFirstFailure)
                .MinMaxLengthWithMessages(textServiceBase, Constants.Validations.PostTextMinLength, Constants.Validations.PostTextMaxLength);
        }
    }
}

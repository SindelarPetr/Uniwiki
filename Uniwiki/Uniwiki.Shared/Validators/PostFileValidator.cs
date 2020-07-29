using System;
using System.Linq;
using FluentValidation;
using Uniwiki.Shared.Extensions;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.Services;

namespace Uniwiki.Shared.Validators
{
    internal class PostFileValidator : AbstractValidator<PostFileDto>
    {
        public PostFileValidator(TextServiceBase textService)
        {
            RuleFor(f => f.NameWithoutExtension).Cascade(CascadeMode.StopOnFirstFailure)
                .MinMaxLengthWithMessages(textService, 1, Constants.Validations.FileNameMaxLength)
                .Must(f => f.All(ch =>
                    Char.IsLetterOrDigit(ch) || Constants.Validations.AllowedFileSpecialCharacters.Contains(ch)))
                .WithMessage(textService.Validation_FileNameContainsNonValidCharacters);
        }
    }
}

﻿using FluentValidation;
using Uniwiki.Shared.Services;

namespace Uniwiki.Shared.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> MinMaxLengthWithMessages<T>(
            this IRuleBuilder<T, string> ruleBuilder, TextServiceBase textServiceBase, int minimumLength,
            int maximumLength)
        {
            return ruleBuilder.MinimumLength(minimumLength).WithMessage(textServiceBase.Validation_MinLength(minimumLength))
                .MaximumLength(maximumLength).WithMessage(textServiceBase.Validation_MaxLength(maximumLength));
        }
    }
}
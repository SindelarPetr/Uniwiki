using FluentValidation;
using FluentValidation.Results;
using Shared.Standardizers;

namespace Shared.Validators
{
    public class StandardizerValidator<TModel> : AbstractValidator<TModel>
    {
        private readonly IStandardizer<TModel> _standardizer;

        public StandardizerValidator(IStandardizer<TModel> standardizer)
        {
            _standardizer = standardizer;
        }

        protected override bool PreValidate(ValidationContext<TModel> context, ValidationResult result)
        {
            var standardizedInstance = _standardizer.Standardize(context.InstanceToValidate);

            var standardizedContext = new ValidationContext<TModel>(standardizedInstance, context.PropertyChain, context.Selector);

            return base.PreValidate(standardizedContext, result);
        }
    }
}

using FluentValidation;
using FluentValidation.Internal;
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

        public override ValidationResult Validate(ValidationContext<TModel> context)
        {
            var standardizedInstance = _standardizer.Standardize(context.InstanceToValidate);

            var standardizedContext = new ValidationContext<TModel>(standardizedInstance, context.PropertyChain, context.Selector);
            
            return base.Validate(standardizedContext);
        }
    }
}

using Shared.Services;
using Shared.Standardizers;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Shared.Standardizers
{
    internal class IsEmailAvailableRequestStandardizer : IStandardizer<IsEmailAvailableRequestDto>
    {
        private readonly IStringStandardizationService _stringStandardizationService;

        public IsEmailAvailableRequestStandardizer(IStringStandardizationService stringStandardizationService)
        {
            _stringStandardizationService = stringStandardizationService;
        }

        public IsEmailAvailableRequestDto Standardize(IsEmailAvailableRequestDto model)
        {
            return new IsEmailAvailableRequestDto(
                _stringStandardizationService.StandardizeEmail(model.Email)
                );
        }
    }
}
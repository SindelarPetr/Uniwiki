using Shared.Extensions;
using Shared.Services;
using Shared.Standardizers;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Shared.Standardizers
{
    public class AddUniversityRequestStandardizer : IStandardizer<AddUniversityRequestDto>
    {
        private readonly IStringStandardizationService _stringStandardizationService;

        public AddUniversityRequestStandardizer(IStringStandardizationService stringStandardizationService)
        {
            _stringStandardizationService = stringStandardizationService;
        }

        public AddUniversityRequestDto Standardize(AddUniversityRequestDto model) =>
            new AddUniversityRequestDto(
                _stringStandardizationService.OptimizeWhiteSpaces(model.FullName).FirstCharToUpper(),
                _stringStandardizationService.OptimizeWhiteSpaces(model.ShortName).FirstCharToUpper(),
                model.Url);
    }
}

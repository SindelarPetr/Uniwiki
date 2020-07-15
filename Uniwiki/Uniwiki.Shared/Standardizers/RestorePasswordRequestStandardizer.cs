using Shared.Services.Abstractions;
using Shared.Standardizers;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Shared.Standardizers
{
    internal class RestorePasswordRequestStandardizer : IStandardizer<RestorePasswordRequestDto>
    {
        private readonly IStringStandardizationService _stringStandardizationService;

        public RestorePasswordRequestStandardizer(IStringStandardizationService stringStandardizationService)
        {
            _stringStandardizationService = stringStandardizationService;
        }

        public RestorePasswordRequestDto Standardize(RestorePasswordRequestDto model)
        {
            return new RestorePasswordRequestDto(
                _stringStandardizationService.StandardizeEmail(model.Email)
                );
        }
    }
}
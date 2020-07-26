using Shared.Services.Abstractions;
using Shared.Standardizers;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Shared.Standardizers
{
    internal class RegisterRequestStandardizer : IStandardizer<RegisterRequestDto>
    {
        private readonly IStringStandardizationService _stringStandardizationService;

        public RegisterRequestStandardizer(IStringStandardizationService stringStandardizationService)
        {
            _stringStandardizationService = stringStandardizationService;
        }

        public RegisterRequestDto Standardize(RegisterRequestDto model)
        {
            return new RegisterRequestDto(
                _stringStandardizationService.StandardizeEmail(model.Email),
                _stringStandardizationService.StandardizeName(model.Name),
                _stringStandardizationService.StandardizeName(model.Surname),
                model.Password,
                model.PasswordAgain,
                model.AgreeToTermsOfUse
                );
        }
    }
}
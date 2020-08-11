using Shared.Services.Abstractions;
using Shared.Standardizers;
using System;
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
            var nameAndSurname = _stringStandardizationService.StandardizeNameAndSurname(model.NameAndSurname);

            return new RegisterRequestDto(
                _stringStandardizationService.StandardizeEmail(model.Email),
                nameAndSurname,
                model.Password,
                model.PasswordAgain,
                model.AgreeToTermsOfUse,
                model.HomeFacultyId,
                model.RecentCourses
                );
        }
    }
}
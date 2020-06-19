using Shared.Services;
using Shared.Standardizers;
using System.Linq;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Shared.Standardizers
{
    internal class LoginRequestStandardizer : IStandardizer<LoginRequestDto>
    {
        private readonly IStringStandardizationService _stringStandardizationService;

        public LoginRequestStandardizer(IStringStandardizationService stringStandardizationService)
        {
            _stringStandardizationService = stringStandardizationService;
        }

        public LoginRequestDto Standardize(LoginRequestDto model)
        {
            // Get recent courses
            var recentCourses = model.RecentCourses.Reverse().Take(Constants.NumberOrRecentCourses).Reverse().ToArray();

            return new LoginRequestDto(_stringStandardizationService.StandardizeEmail(model.Email), model.Password, recentCourses);
        }
    }
}
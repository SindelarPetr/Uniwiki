using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.ServerActions.Authentication
{
    internal class LoginServerAction : ServerActionBase<LoginRequestDto, LoginResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly TextService _textService;
        private readonly CourseVisitRepository _courseVisitRepository;
        private readonly CourseRepository _courseRepository;
        private readonly ITimeService _timeService;
        private readonly ILoginService _loginService;
        private readonly RecentCoursesService _recentCoursesService;
        private readonly UniwikiContext _uniwikiContext;

        public LoginServerAction(IServiceProvider serviceProvider, TextService textService, CourseVisitRepository courseVisitRepository, CourseRepository courseRepository, ITimeService timeService, ILoginService loginService, RecentCoursesService recentCoursesService, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _textService = textService;
            _courseVisitRepository = courseVisitRepository;
            _courseRepository = courseRepository;
            _timeService = timeService;
            _loginService = loginService;
            _recentCoursesService = recentCoursesService;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<LoginResponseDto> ExecuteAsync(LoginRequestDto request, RequestContext context)
        {
            // Issue the token
            var token = _loginService.LoginUser(request.Email, request.Password);

            var tokenDto = token.ToLoginTokenDto().Single();

            // TODO (uncomment): Set the recent courses
            // _recentCoursesService.SetAsRecentCourses(request.RecentCourses, tokenDto.ProfileId);

            // Get the authorized user
            var authorizedUser = _uniwikiContext
                .Profiles
                .Where(p => p.Id == tokenDto.ProfileId)
                .ToAuthorizedUserDto();

            // Create response
            var response = new LoginResponseDto(tokenDto, authorizedUser);

            return Task.FromResult(response);
        }
    }
}
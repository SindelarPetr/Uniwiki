using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Server.Application.ServerActions.Authentication
{
    internal class LoginServerAction : ServerActionBase<LoginRequestDto, LoginResponseDto>
    {
        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        private readonly TextService _textService;
        private readonly ICourseVisitRepository _courseVisitRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ITimeService _timeService;
        private readonly ILoginService _loginService;
        private readonly IRecentCoursesService _recentCoursesService;

        public LoginServerAction(IServiceProvider serviceProvider, TextService textService, ICourseVisitRepository courseVisitRepository, ICourseRepository courseRepository, ITimeService timeService, ILoginService loginService, IRecentCoursesService recentCoursesService) : base(serviceProvider)
        {
            _textService = textService;
            _courseVisitRepository = courseVisitRepository;
            _courseRepository = courseRepository;
            _timeService = timeService;
            _loginService = loginService;
            _recentCoursesService = recentCoursesService;
        }

        protected override Task<LoginResponseDto> ExecuteAsync(LoginRequestDto request, RequestContext context)
        {
            // Issue the token
            var token = _loginService.LoginUser(request.Email, request.Password);

            // Set the recent courses
            _recentCoursesService.SetAsRecentCourses(request.RecentCourses, token.Profile);

            // Create response
            var response = new LoginResponseDto(token.ToDto(), token.Profile.ToDto());

            return Task.FromResult(response);
        }
    }
}
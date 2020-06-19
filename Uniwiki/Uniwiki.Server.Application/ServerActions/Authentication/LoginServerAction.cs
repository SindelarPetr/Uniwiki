using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared.RequestResponse.Authentication;
using Uniwiki.Shared.Services.Abstractions;

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

        public LoginServerAction(IServiceProvider serviceProvider, TextService textService, ICourseVisitRepository courseVisitRepository, ICourseRepository courseRepository, ITimeService timeService, ILoginService loginService) : base(serviceProvider)
        {
            _textService = textService;
            _courseVisitRepository = courseVisitRepository;
            _courseRepository = courseRepository;
            _timeService = timeService;
            _loginService = loginService;
        }

        protected override Task<LoginResponseDto> ExecuteAsync(LoginRequestDto request, RequestContext context)
        {
            // Issue the token
            var token = _loginService.LoginUser(request.Email, request.Password);

            // Get recent courses
            var recentCourses = _courseRepository.TryGetCourses(request.RecentCourses.Select(c => (c.Url, c.StudyGroup.Url, c.University.Url)));

            // Set the recent courses
            _courseVisitRepository.AddRecentCourseVisits(recentCourses, token.Profile, _timeService.Now);

            // Create response
            var response = new LoginResponseDto(token.ToDto(), token.Profile.ToDto());

            return Task.FromResult(response);
        }
    }
}
using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class ProvideFeedbackServerAction : ServerActionBase<ProvideFeedbackRequestDto, ProvideFeedbackResponseDto>
    {
        private readonly FeedbackRepository _feedbackRepository;
        private readonly ITimeService _timeService;
        private readonly UniwikiContext _uniwikiContext;

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        public ProvideFeedbackServerAction(IServiceProvider serviceProvider, FeedbackRepository feedbackRepository, ITimeService timeService, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _feedbackRepository = feedbackRepository;
            _timeService = timeService;
            _uniwikiContext = uniwikiContext;
        }

        protected override Task<ProvideFeedbackResponseDto> ExecuteAsync(ProvideFeedbackRequestDto request, RequestContext context)
        {
            // Add the feedback to the DB
            _feedbackRepository.AddFeedback(context.UserId, request.Rating, request.Text, _timeService.Now);

            // Find the user
            var user = context.UserId != null ? _uniwikiContext.Profiles.Where(p => p.Id == context.UserId.Value).ToAuthorizedUserDto() : null;

            // Create response
            var response = new ProvideFeedbackResponseDto(user);

            return Task.FromResult(response);
        }
    }
}

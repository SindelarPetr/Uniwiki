using Server.Appliaction.ServerActions;
using Shared.Services.Abstractions;
using System;
using System.Threading.Tasks;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class ProvideFeedbackServerAction : ServerActionBase<ProvideFeedbackRequestDto, ProvideFeedbackResponseDto>
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly ITimeService _timeService;

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.None;

        public ProvideFeedbackServerAction(IServiceProvider serviceProvider, IFeedbackRepository feedbackRepository, ITimeService timeService) : base(serviceProvider)
        {
            _feedbackRepository = feedbackRepository;
            _timeService = timeService;
        }

        protected override Task<ProvideFeedbackResponseDto> ExecuteAsync(ProvideFeedbackRequestDto request, RequestContext context)
        {
            // Save the feedback to the DB
            _feedbackRepository.CreateFeedback(context.User, request.Rating, request.Text, context.IpAddress.ToString(), _timeService.Now);

            // Create response
            var response = new ProvideFeedbackResponseDto(context.User?.ToDto());

            return Task.FromResult(response);
        }
    }
}

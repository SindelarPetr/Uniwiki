using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Services.Abstractions;
using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Shared;
using Uniwiki.Shared.Tests.FakeServices;

namespace Uniwiki.Server.Persistence.Tests
{
    [TestClass]
    public class BasicTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            ServiceCollection services = new ServiceCollection();

            var timeService = new FakeTimeService(new DateTime(2020, 3, 12));

            services.AddSingleton<ITimeService>(timeService);

            services.AddUniwikiServerPersistence();

            var provider = services.BuildServiceProvider();

            var feedbackRepository = provider.GetService<IFeedbackRepository>();

            var feedback = new FeedbackModel(Guid.NewGuid(), false, null, 3, "ffff", timeService.Now);

            feedbackRepository.Add(feedback);
        }
    }
}

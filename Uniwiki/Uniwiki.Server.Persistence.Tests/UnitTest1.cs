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

            var feedbackText = "Some god damn feedback";
            var feedbackRating = 2;
            ProfileModel? feedbackUser = null;

            var provider = services.BuildServiceProvider();

            // Create a new feedback          
            FeedbackModel createdFeedback;  
            using (var scope = provider.CreateScope())
            {
                var feedbackRepository = scope.ServiceProvider.GetService<IFeedbackRepository>();
                
                createdFeedback = feedbackRepository.AddFeedback(feedbackUser, feedbackRating, feedbackText, timeService.Now);
            }

            // Load the feedback
            FeedbackModel loadedFeedback;
            using (var scope = provider.CreateScope())
            {
                var feedbackRepository = scope.ServiceProvider.GetService<IFeedbackRepository>();

                loadedFeedback = feedbackRepository.FindById(createdFeedback.Id);
            }

            // TODO: Assert comparison of the properties in this createdFeedback and loadedFeedback all should match
            
        }
    }
}

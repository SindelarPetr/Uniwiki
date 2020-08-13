using KellermanSoftware.CompareNetObjects;
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
    public abstract class RepositoryBase<TRepository, TModel, TId> 
    {
        private ServiceProvider _baseServiceProvider = null!;
        protected IServiceProvider ServiceProvider => _scope.ServiceProvider;
        private IServiceScope _scope = null!;

        [ClassInitialize]
        protected virtual void ClassInitialize(Action<ServiceCollection>? configureServices = null)
        {

            ServiceCollection services = new ServiceCollection();
            services.AddUniwikiServerPersistence();

            var timeService = new FakeTimeService(new DateTime(2020, 3, 12));
            services.AddSingleton<ITimeService>(timeService);

            configureServices?.Invoke(services);

            _baseServiceProvider = services.BuildServiceProvider();
        }

        [TestInitialize]
        protected virtual void TestInitialize()
        {
            if (_scope != null) 
                _scope.Dispose();

            _scope = _baseServiceProvider.CreateScope();
        }


    }

    [TestClass]
    public class BasicTest
    {
        [TestMethod]
        public void TestMethod1()
        {
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

            // Assert comparison of the properties in this createdFeedback and loadedFeedback all should match
            var compareLogic = new CompareLogic();
            compareLogic.Config.CompareChildren = false;

            var result = compareLogic.Compare(createdFeedback, loadedFeedback);

            Assert.IsTrue(result.AreEqual, result.DifferencesString);
        }
    }
}

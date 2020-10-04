using KellermanSoftware.CompareNetObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Services.Abstractions;
using System;
using System.Data;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Shared;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Shared;
using Uniwiki.Shared.Tests.FakeServices;

namespace Uniwiki.Server.Persistence.Tests
{
    [TestClass]
    public abstract class RepositoryTestBase<TRepository, TModel, TId>
    {
        private ServiceProvider _baseServiceProvider = null!;
        protected IServiceProvider ServiceProvider => _scope.ServiceProvider;
        private IServiceScope _scope = null!;

        //[ClassInitialize]
        protected virtual void ClassInitialize(Action<ServiceCollection>? configureServices = null)
        {

            //ServiceCollection services = new ServiceCollection();
            //services.AddUniwikiServerPersistence();

            //var timeService = new FakeTimeService(new DateTime(2020, 3, 12));
            //services.AddSingleton<ITimeService>(timeService);

            //configureServices?.Invoke(services);

            //_baseServiceProvider = services.BuildServiceProvider();
        }

        //[TestInitialize]
        protected virtual void TestInitialize()
        {
            if (_scope != null)
            {
                _scope.Dispose();
            }

            _scope = _baseServiceProvider.CreateScope();
        }


    }

    [TestClass]
    public class BasicTest
    {
        [TestMethod]
        public void TestSoftDelete()
        {
            //var keepAliveConnection = new SqliteConnection("DataSource=:memory:");
            //keepAliveConnection.Open();

            var loggerFactory = LoggerFactory.Create(builder => {
                builder
                .AddConsole((options) => { })
                    .AddFilter((category, level) =>
                            category == DbLoggerCategory.Database.Command.Name
                            && level == LogLevel.Information);
            });

            var services = new ServiceCollection();

            services.AddUniwikiServerPersistence(options => {
                //options.UseSqlite(keepAliveConnection);
                options.UseSqlServer(
                    "Data Source=PSINDELAR01\\SQLEXPRESS01;Initial Catalog=UniwikiLocalDatabase;Integrated Security=True");
                options.EnableSensitiveDataLogging();
                options.UseLoggerFactory(loggerFactory);
            });

            var provider = services.BuildServiceProvider();

            using (var scope = provider.CreateScope())
            {
                using var uniwikiContext = scope.ServiceProvider.GetRequiredService<UniwikiContext>();

                uniwikiContext.Database.EnsureDeleted();
                uniwikiContext.Database.EnsureCreated();
            }

            var postId = Guid.NewGuid();
            using (var scope = provider.CreateScope())
            {
                using var uniwikiContext = scope.ServiceProvider.GetRequiredService<UniwikiContext>();

                var profile = uniwikiContext.Profiles.Add(new ProfileModel(Guid.NewGuid(), "KOKO", "petr", "Sindelar",
                    "petr-sindelar", "koko", new byte[0], "--", DateTime.Now, true, AuthenticationLevel.PrimaryToken,
                    null));

                var university = uniwikiContext.Universities.Add(new UniversityModel(Guid.NewGuid(), "České vysok..",
                    "Short uni name", "cvut", false));

                var studyGroup = uniwikiContext.StudyGroups.Add(new StudyGroupModel(Guid.NewGuid(),
                    university.Entity.Id,
                    "KOKO", "some tudy grou", "eeee", Language.Czech, false));

                var course = uniwikiContext.Courses.Add(new CourseModel(Guid.NewGuid(), "code", "code",
                    "full course name",
                    "full name", profile.Entity.Id, studyGroup.Entity.Id, "uni", "course", "studyg", false));

                var post = uniwikiContext.Posts.Add(new PostModel(postId, "sometype", profile.Entity.Id,
                    "Some text yeah", course.Entity.Id, false, DateTime.Now));
                var postTwo = uniwikiContext.Posts.Add(new PostModel(Guid.NewGuid(), "sometype", profile.Entity.Id,
                    "Some text yeah", course.Entity.Id, true, DateTime.Now));

                var comment = uniwikiContext.PostComments.Add(new PostCommentModel(Guid.NewGuid(), profile.Entity.Id,
                    post.Entity.Id, post.Entity.Text, DateTime.Now));

                uniwikiContext.SaveChanges();

                var posts = uniwikiContext.Posts.Count();

                uniwikiContext.PostComments.Remove(comment.Entity);
                uniwikiContext.SaveChanges();

                var commentAgain = uniwikiContext
                    .PostComments
                    .Where(c => c.Id == comment.Entity.Id)
                    .SingleOrDefault();

                Assert.AreEqual(null, commentAgain);
            //}

            //uniwikiContext
            //    .ChangeTracker
            //    .Entries()
            //    .ToList()
            //    .ForEach(e => e.State = EntityState.Detached);

            post.State = EntityState.Detached;

            //using (var scope = provider.CreateScope())
            //{
                //using var uniwikiContext = scope.ServiceProvider.GetRequiredService<UniwikiContext>();

                //foreach (var entityEntry in uniwikiContext.ChangeTracker.Entries())
                //{
                //    entityEntry.State = EntityState.Detached;
                //}

                // Get post and check that it has no comments 
                var postAgain = uniwikiContext
                    .Posts
                    .Include(p => p.Comments)
                    .First(p => p.Id == postId);

                Assert.AreEqual(0, postAgain.Comments.Count);
            }
        }


        //[TestMethod]
        public void TestMethod1()
        {
            //var feedbackText = "Some god damn feedback";
            //var feedbackRating = 2;
            //ProfileModel? feedbackUser = null;

            //ServiceCollection services = new ServiceCollection();
            //services.AddUniwikiServerPersistence();

            //var timeService = new FakeTimeService(new DateTime(2020, 3, 12));
            //services.AddSingleton<ITimeService>(timeService);
            //var provider = services.BuildServiceProvider();

            //// Create a new feedback          
            //FeedbackModel createdFeedback;
            //using (var scope = provider.CreateScope())
            //{
            //    var feedbackRepository = scope.ServiceProvider.GetService<FeedbackRepository>();

            //    createdFeedback = feedbackRepository.AddFeedback(feedbackUser, feedbackRating, feedbackText, timeService.Now);
            //}

            //// Load the feedback
            //FeedbackModel loadedFeedback;
            //using (var scope = provider.CreateScope())
            //{
            //    var feedbackRepository = scope.ServiceProvider.GetService<FeedbackRepository>();

            //    loadedFeedback = feedbackRepository.FindById(createdFeedback.Id);
            //}

            //// Assert comparison of the properties in this createdFeedback and loadedFeedback all should match
            //var compareLogic = new CompareLogic();
            //compareLogic.Config.CompareChildren = false;

            //var result = compareLogic.Compare(createdFeedback, loadedFeedback);

            //Assert.IsTrue(result.AreEqual, result.DifferencesString);
        }
    }
}

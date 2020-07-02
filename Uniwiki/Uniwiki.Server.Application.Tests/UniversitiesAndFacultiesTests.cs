using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Appliaction.ServerActions;
using Shared;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Uniwiki.Server.Application.ServerActions;
using Uniwiki.Server.Application.ServerActions.Authentication;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.InMemory.Services;
using Uniwiki.Shared;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.RequestResponse.Authentication;
using Uniwiki.Shared.Services.Abstractions;
using Uniwiki.Shared.Tests.FakeServices;

namespace Uniwiki.Server.Application.Tests
{
    [TestClass]
    public class UniversitiesAndFacultiesTests
    {
        [TestMethod]
        public async Task RunPostComplexTest()
        {
            // --------- Arrange
            ServiceCollection services = new ServiceCollection();
            services.AddUniwikiServerApplicationServices();

            // Fake time service
            var timeService = new FakeTimeService(new DateTime(2020, 06, 30, 14, 47, 56));
            services.AddSingleton<ITimeService>(timeService);

            // Fake email service
            var emailService = new FakeEmailService();
            services.AddTransient<IEmailService>(p => emailService);

            var provider = services.BuildServiceProvider();
            var anonymousContext = new RequestContext(null, AuthenticationLevel.None, Guid.NewGuid(), Language.English);
            var registerServerAction = provider.GetService<RegisterServerAction>();
            var confirmEmailServerAction = provider.GetService<ConfirmEmailServerAction>();
            var resendConfirmationEmailServerAction = provider.GetService<ResendConfirmationEmailServerAction>();
            var loginServerAction = provider.GetService<LoginServerAction>();
            var changePasswordServerAction = provider.GetService<ChangePasswordServerAction>();
            var authenticationService = provider.GetService<IAuthenticationService>();
            var getSearchResultsServerAction = provider.GetService<GetSearchResultsServerAction>();
            var dataManipulationService = provider.GetService<IDataManipulationService>();
            var addUniversityServerAction = provider.GetService<AddUniversityServerAction>();
            var addStudyGroupServerAction = provider.GetService<AddStudyGroupServerAction>();
            var addCourseServerAction = provider.GetService<AddCourseServerAction>();
            var getCourseServerAction = provider.GetService<GetCourseServerAction>();
            var dataService = provider.GetService<DataService>();
            var addPostServerAction = provider.GetService<AddPostServerAction>();
            var editPostServerAction = provider.GetService<EditPostServerAction>();
            var removePostServerAction = provider.GetService<RemovePostServerAction>();
            var likePostServerAction = provider.GetService<LikePostServerAction>();
            var addCommentServerAction = provider.GetService<AddCommentServerAction>();
            var likePostCommentServerAction = provider.GetService<LikePostCommentServerAction>();

            var user1Email = "petr@gmail.com";
            var user1Password = "Password";

            var user2Email = "martin@gmail.com";
            var user2Password = "ssword";

            var course1Name = "Linear Algebra";
            var course1Code = "BI-LIN";

            var course2Name = "3D Print";
            var course2Code = "BI-3D";

            var postCategory1 = "This is a custom cat1";
            var postCategory2 = "This is a cat2";

            var postText1 = "This is some text";
            var postText2 = "This is some other text";

            var comment1Text = "Some comment.";

            // Register the first user in the way, that he is an admin
            var adminContext = await dataManipulationService.RegisterUser(user1Email, "Some", "Testuser", user1Password, isAdmin: true);
            var user1Context = await dataManipulationService.RegisterUser(user2Email, "Martin", "Another", user2Password, isAdmin: false);
            var user2Context = await dataManipulationService.RegisterUser("SomeOther@email.com", "Other", "User", user2Password, isAdmin: false);

            // --------- Act + Assert

            // TEST: Only admin can add a university
            var addCtuRequest = new AddUniversityRequestDto("Czech Technical University", "CTU", "ctu");
            await Assert.ThrowsExceptionAsync<RequestException>(() => addUniversityServerAction.ExecuteActionAsync(addCtuRequest, anonymousContext));
            await Assert.ThrowsExceptionAsync<RequestException>(() => addUniversityServerAction.ExecuteActionAsync(addCtuRequest, user1Context));
            var addCtuResponse = await addUniversityServerAction.ExecuteActionAsync(addCtuRequest, adminContext);
            var university1 = addCtuResponse.University;

            // TEST: Only admin can add a faculty
            var addStudyGroup1Request = new AddStudyGroupRequestDto("Faculty of Information Technology", "FIT", addCtuResponse.University.Id, Language.English);
            await Assert.ThrowsExceptionAsync<RequestException>(() => addStudyGroupServerAction.ExecuteActionAsync(addStudyGroup1Request, anonymousContext));
            await Assert.ThrowsExceptionAsync<RequestException>(() => addStudyGroupServerAction.ExecuteActionAsync(addStudyGroup1Request, user1Context));
            var addStudyGroup1Response = await addStudyGroupServerAction.ExecuteActionAsync(addStudyGroup1Request, adminContext);
            var studyGroup1 = addStudyGroup1Response.StudyGroupDto;

            var addStudyGroup2Request = new AddStudyGroupRequestDto("Faculty of Architecture", "FA", addCtuResponse.University.Id, Language.Czech);
            var addStudyGroup2Response = await addStudyGroupServerAction.ExecuteActionAsync(addStudyGroup2Request, adminContext);
            var studyGroup2 = addStudyGroup2Response.StudyGroupDto;


            // TEST: It is possible to add a course (add 2 courses)
            var addCourse1Request = new AddCourseRequestDto(course1Name, course1Code, studyGroup1.Id);
            var addCourse1Response = await addCourseServerAction.ExecuteActionAsync(addCourse1Request, adminContext);
            var course1 = addCourse1Response.CourseDto;
            var addCourse2Request = new AddCourseRequestDto(course2Name, course2Code, studyGroup1.Id);
            var addCourse2Response = await addCourseServerAction.ExecuteActionAsync(addCourse2Request, adminContext);
            var course2 = addCourse2Response.CourseDto;


            // TEST: Cannot create a course with the same name twice (in the same study group)
            var addCourseErr1Request = new AddCourseRequestDto(course1Name.ToLower(), course1Code.ToLower(), studyGroup1.Id);
            await Assert.ThrowsExceptionAsync<RequestException>(() => addCourseServerAction.ExecuteActionAsync(addCourseErr1Request, adminContext));


            // TEST: Can create a new course in a study group with the same name as a course in a different study group
            var addCourse3Request = new AddCourseRequestDto(course1Name.ToLower(), course1Code.ToLower(), studyGroup2.Id);
            var addCourse3Response = await addCourseServerAction.ExecuteActionAsync(addCourse3Request, adminContext);
            var course3 = addCourse3Response.CourseDto;


            // TEST: Empty search for a course of unathorised user gives empty results
            var getSearchResults1Request = new GetSearchResultsRequestDto(string.Empty, null, null);
            var getSearchResults1Response = await getSearchResultsServerAction.ExecuteActionAsync(getSearchResults1Request, anonymousContext);
            Assert.IsFalse(getSearchResults1Response.Courses.Any());


            // TEST: Empty search for a course of unathorised user gives empty recent courses
            Assert.IsFalse(getSearchResults1Response.RecentCourses.Any());


            // TEST: Empty search for a course with a selected faculty of unathorised user gives all courses in the faculty
            var getSearchResults2Request = new GetSearchResultsRequestDto(string.Empty, university1.Id, studyGroup1.Id);
            var getSearchResults2Response = await getSearchResultsServerAction.ExecuteActionAsync(getSearchResults2Request, anonymousContext);
            Assert.AreEqual(2, getSearchResults2Response.Courses.Count());
            Assert.IsTrue(getSearchResults2Response.Courses.Any(c => c.Id == course1.Id));
            Assert.IsTrue(getSearchResults2Response.Courses.Any(c => c.Id == course2.Id));


            // TEST: Can search for a course by typing text
            var getSearchResults3Request = new GetSearchResultsRequestDto(course1Name.Substring(0, 3), null, null);
            var getSearchResults3Response = await getSearchResultsServerAction.ExecuteActionAsync(getSearchResults3Request, anonymousContext);
            Assert.AreEqual(2, getSearchResults3Response.Courses.Count());
            Assert.IsTrue(getSearchResults3Response.Courses.Any(c => c.Id == course1.Id));
            Assert.IsTrue(getSearchResults3Response.Courses.Any(c => c.Id == course3.Id));


            // TEST: Can search for a course by typing text with courses filtered by faculty
            var getSearchResults4Request = new GetSearchResultsRequestDto(course1Name.Substring(0, 3), university1.Id, studyGroup1.Id);
            var getSearchResults4Response = await getSearchResultsServerAction.ExecuteActionAsync(getSearchResults4Request, anonymousContext);
            Assert.AreEqual(1, getSearchResults4Response.Courses.Count());
            Assert.IsTrue(getSearchResults4Response.Courses.Any(c => c.Id == course1.Id));

            // TEST: Recent courses are not visible for any of previous searches
            Assert.IsFalse(getSearchResults1Response.RecentCourses.Any());
            Assert.IsFalse(getSearchResults2Response.RecentCourses.Any());
            Assert.IsFalse(getSearchResults3Response.RecentCourses.Any());
            Assert.IsFalse(getSearchResults4Response.RecentCourses.Any());

            // TEST: Can get the right course
            var getCourse1Request = new GetCourseRequestDto(university1.Url, studyGroup1.Url, course1.Url, null, true, Constants.MaxPostsToFetch);
            var getCourse1Response = await getCourseServerAction.ExecuteActionAsync(getCourse1Request, user1Context);
            Assert.AreEqual(getCourse1Response.Course.Id, course1.Id);

            // TEST: Recent courses are visible if search text is not typed 
            var getSearchResults5Request = new GetSearchResultsRequestDto(string.Empty, null, null);
            var getSearchResults5Response = await getSearchResultsServerAction.ExecuteActionAsync(getSearchResults5Request, user1Context);
            Assert.AreEqual(1, getSearchResults5Response.RecentCourses.Count());
            Assert.IsTrue(getSearchResults5Response.RecentCourses.Any(c => c.Id == course1.Id));

            // TEST: Recent courses are not visible if search text is typed
            var getSearchResults6Request = new GetSearchResultsRequestDto("Some t", null, null);
            var getSearchResults6Response = await getSearchResultsServerAction.ExecuteActionAsync(getSearchResults6Request, user1Context);
            Assert.IsFalse(getSearchResults6Response.RecentCourses.Any());

            // TEST: Can get course information which will include the default post types (for new post) of the language of the study group - check 2 languages
            var getCourse2Request = new GetCourseRequestDto(university1.Url, studyGroup1.Url, course1.Url, null, true, Constants.MaxPostsToFetch);
            var getCourse2Response = await getCourseServerAction.ExecuteActionAsync(getCourse2Request, user1Context);
            CollectionAssert.AreEquivalent(getCourse2Response.NewPostPostTypes, dataService._defaultPostTypesEn.ToArray());

            var getCourse3Request = new GetCourseRequestDto(university1.Url, studyGroup2.Url, course3.Url, null, true, Constants.MaxPostsToFetch);
            var getCourse3Response = await getCourseServerAction.ExecuteActionAsync(getCourse3Request, user1Context);
            CollectionAssert.AreEquivalent(getCourse3Response.NewPostPostTypes, dataService._defaultPostTypesCz.ToArray());

            // TEST: Can add a post without a category
            var addPost1Request = new AddPostRequestDto(postText1, null, course1.Id, new Shared.ModelDtos.PostFileDto[0]);
            var addPost1Response = await addPostServerAction.ExecuteActionAsync(addPost1Request, user1Context);
            var post1 = addPost1Response.NewPost;

            // TEST: Response from getting a course should contain possibility to filter by null
            var getCourse4Request = new GetCourseRequestDto(university1.Url, studyGroup1.Url, course1.Url, null, true, Constants.MaxPostsToFetch);
            var getCourse4Response = await getCourseServerAction.ExecuteActionAsync(getCourse4Request, user1Context);
            CollectionAssert.AreEquivalent(getCourse4Response.NewPostPostTypes, dataService._defaultPostTypesEn.ToArray(), "null Category is not a possibility to use in a new post.");
            Assert.IsTrue(getCourse4Response.FilterPostTypes.Count() == 1);
            Assert.IsTrue(getCourse4Response.FilterPostTypes.First().PostType == null);
            Assert.IsTrue(getCourse4Response.FilterPostTypes.First().Count == 1);

            // TEST: Can add a post with a custom category
            var addPost2Request = new AddPostRequestDto("random post text2", postCategory1, course1.Id, new Shared.ModelDtos.PostFileDto[0]);
            var addPost2Response = await addPostServerAction.ExecuteActionAsync(addPost2Request, user1Context);
            var post2 = addPost2Response.NewPost;
            Assert.IsNotNull(post2.PostType);
            Assert.AreEqual(postCategory1.ToLower(), post2.PostType.ToLower());

            // TEST: If there is a non-default Category of a post, it will be shown as a possibility to filter and create a new post in the course page
            var getCourse5Request = new GetCourseRequestDto(university1.Url, studyGroup1.Url, course1.Url, null, true, Constants.MaxPostsToFetch);
            var getCourse5Response = await getCourseServerAction.ExecuteActionAsync(getCourse5Request, user1Context);
            CollectionAssert.Contains(getCourse5Response.NewPostPostTypes, postCategory1);
            CollectionAssert.Contains(getCourse5Response.FilterPostTypes.Select(pt => pt.PostType).ToArray(), postCategory1);

            // TEST: If there is a non-default Category of a post in one course, it will not be shown as a possibility to filter or add a new post in another course page
            var getCourse6Request = new GetCourseRequestDto(university1.Url, studyGroup1.Url, course2.Url, null, true, Constants.MaxPostsToFetch);
            var getCourse6Response = await getCourseServerAction.ExecuteActionAsync(getCourse6Request, user1Context);
            CollectionAssert.DoesNotContain(getCourse6Response.NewPostPostTypes, postCategory1);
            CollectionAssert.DoesNotContain(getCourse6Response.FilterPostTypes, postCategory1);

            // TEST: Can edit text and Category of the post
            var editPost1Request = new EditPostRequestDto(post2.Id, postText2, postCategory2, new Shared.ModelDtos.PostFileDto[0]);
            var editPost1Response = await editPostServerAction.ExecuteActionAsync(editPost1Request, user1Context);
            post2 = editPost1Response.EdittedPost; // Not introducin new var, because we editted the old post
            Assert.AreEqual(postText2, post2.Text);
            Assert.AreEqual(postCategory2, post2.PostType);

            // TEST: The custom category does not appear on neither of the lists ('filter' list and 'make new post' list) after editiing the post (because there is no more post left with such category)
            var getCourse7Request = new GetCourseRequestDto(university1.Url, studyGroup1.Url, course1.Url, null, true, Constants.MaxPostsToFetch);
            var getCourse7Response = await getCourseServerAction.ExecuteActionAsync(getCourse7Request, user1Context);
            CollectionAssert.DoesNotContain(getCourse7Response.NewPostPostTypes, postCategory1);
            CollectionAssert.DoesNotContain(getCourse7Response.FilterPostTypes, postCategory1);

            // TEST: Can filter by a Category in the course page - filtering will show posts only with the filtered category
            var getCourse8Request = new GetCourseRequestDto(university1.Url, studyGroup1.Url, course1.Url, postCategory2, false, Constants.MaxPostsToFetch);
            var getCourse8Response = await getCourseServerAction.ExecuteActionAsync(getCourse8Request, user1Context);
            Assert.IsTrue(getCourse8Response.Posts.Length == 1);
            Assert.AreEqual(post2.Id, getCourse8Response.Posts.First().Id);

            // TEST: Can like a post
            var likePost1Request = new LikePostRequestDto(post1.Id);
            var likePost1Response = await likePostServerAction.ExecuteActionAsync(likePost1Request, user1Context);
            Assert.IsTrue(likePost1Response.Post.LikedByClient);
            Assert.AreEqual(1, likePost1Response.Post.LikesCount);

            // TEST: Can write a comment
            var addComment1Request = new AddCommentRequestDto(post1.Id, comment1Text);
            var addComment1Response = await addCommentServerAction.ExecuteActionAsync(addComment1Request, user1Context);
            Assert.AreEqual(1, addComment1Response.Post.PostComments.Length);
            Assert.AreEqual(comment1Text, addComment1Response.Post.PostComments.Last().Text);
            var comment1 = addComment1Response.Post.PostComments.Last();

            // TEST: Unauthorised user cannot write a comment
            var addComment2Request = new AddCommentRequestDto(post1.Id, "Some another comment.");
            await Assert.ThrowsExceptionAsync<RequestException>(() => addCommentServerAction.ExecuteActionAsync(addComment2Request, anonymousContext));

            // TEST: Can like a comment
            var likeComment1Request = new LikePostCommentRequestDto(comment1.Id);
            var likeComment1Response = await likePostCommentServerAction.ExecuteActionAsync(likeComment1Request, user1Context);
            Assert.IsTrue(likeComment1Response.Post.PostComments.First().LikedByClient);
            Assert.AreEqual(1, likeComment1Response.Post.PostComments.First().LikesCount);

            // TEST: A post can not be removed by non-owner
            var removePost1Request = new RemovePostRequestDto(post2.Id);
            await Assert.ThrowsExceptionAsync<RequestException>(() => removePostServerAction.ExecuteActionAsync(removePost1Request, user2Context));

            // TEST: A post can be removed by any admin
            var removePost2Request = new RemovePostRequestDto(post2.Id);
            await removePostServerAction.ExecuteActionAsync(removePost2Request, adminContext);

            // TEST: A post can be removed by its owner (registered user)
            var removePost3Request = new RemovePostRequestDto(post1.Id);
            await removePostServerAction.ExecuteActionAsync(removePost3Request, user1Context);

            // TEST: The removed category of a post will not be showed in the course page as a filter option
            var getCourse9Request = new GetCourseRequestDto(university1.Url, studyGroup1.Url, course1.Url, null, true, Constants.MaxPostsToFetch);
            var getCourse9Response = await getCourseServerAction.ExecuteActionAsync(getCourse9Request, user1Context);
            Assert.IsFalse(getCourse9Response.Posts.Any());

            // TEST: The category of removed post will not be shown in the add post and neither in the filter list
            CollectionAssert.AreEquivalent(dataService._defaultPostTypesEn.ToArray(), getCourse9Response.NewPostPostTypes);
            Assert.IsFalse(getCourse9Response.FilterPostTypes.Any());
        }
    }
}

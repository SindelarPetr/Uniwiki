using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Appliaction.ServerActions;
using Shared;
using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Shared.Exceptions;
using Uniwiki.Server.Application.ServerActions;
using Uniwiki.Server.Application.ServerActions.Authentication;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.RequestResponse.Authentication;
using Uniwiki.Shared.Tests.FakeServices;
using Shared.Services.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Shared.Tests;

namespace Uniwiki.Server.Application.Tests
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public async Task RunPostComplexTest()
        {
            // TODO: Everything is just copied from authentication tests
            // --------- Arrange
            ServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddUniwikiServerApplicationServices();
            services.AddSingleton<IHostingEnvironment, FakeHostingEnvironment>();

            // Fake time service
            var timeService = new FakeTimeService(new DateTime(2020, 06, 30, 14, 47, 56));
            services.AddSingleton<ITimeService>(timeService);

            // Fake email service
            var emailService = new FakeEmailService();
            services.AddTransient<IEmailService>(p => emailService);

            var provider = services.BuildServiceProvider();
            var anonymousContext = new RequestContext(null, AuthenticationLevel.None, Guid.NewGuid().ToString(), Language.English, new System.Net.IPAddress(0x2414188f));
            var registerServerAction = provider.GetService<RegisterServerAction>();
            var confirmEmailServerAction = provider.GetService<ConfirmEmailServerAction>();
            var resendConfirmationEmailServerAction = provider.GetService<ResendConfirmationEmailServerAction>();
            var loginServerAction = provider.GetService<LoginServerAction>();
            var changePasswordServerAction = provider.GetService<ChangePasswordServerAction>();
            var authenticationService = provider.GetService<IAuthenticationService>();
            var getSearchResultsServerAction = provider.GetService<GetSearchResultsServerAction>();

            var user1Email = "petr.svetr@gmail.com";
            var user1Password = "Password";
            var user1NewPassword = "NewPassword";

            // --------- Act + Assert

            // TEST: Register the user
            var userDto1 = (await registerServerAction.ExecuteActionAsync(new RegisterRequestDto(user1Email, "Some Testuser", user1Password, user1Password, true, null, new CourseDto[0]), anonymousContext)).UserProfile;
            var confirmationSecret1 = emailService.RegisterSecrets.Last();
            Assert.IsNotNull(confirmationSecret1, "Confirmation email has to be sent.");

            // TEST: Repeated registration with the same email results in an exception
            await Assert.ThrowsExceptionAsync<RequestException>(
                () => registerServerAction.ExecuteActionAsync(
                    new RegisterRequestDto(user1Email, "Somsse Testussser", user1Password, user1Password, true, null, new CourseDto[0])
                    , anonymousContext)
                );


            // TEST: Repeated registration with the same email after some time results in resending the confirmation secret
            timeService.MoveTime(Constants.ResendRegistrationEmailMinTime.Add(TimeSpan.FromSeconds(5))); // Move time
            var userDto2 = (await registerServerAction.ExecuteActionAsync(new RegisterRequestDto(user1Email, "Some Testuser", user1Password, user1Password, true, null, new CourseDto[0]), anonymousContext)).UserProfile;
            var confirmationSecret2 = emailService.RegisterSecrets.Last();
            Assert.IsNotNull(confirmationSecret2, "Confirmation email has to be sent.");


            // TEST: User cannot ask for a new confirmation email too soon
            await Assert.ThrowsExceptionAsync<RequestException>(() => resendConfirmationEmailServerAction.ExecuteActionAsync(new ResendConfirmationEmailRequestDto(user1Email), anonymousContext));


            // TEST: User can ask for a new confirmation email after some time
            timeService.MoveTime(Constants.ResendRegistrationEmailMinTime.Add(TimeSpan.FromSeconds(5))); // Move time
            var userDto3 = (await registerServerAction.ExecuteActionAsync(new RegisterRequestDto(user1Email, "Some Testuser", user1Password, user1Password, true, null, new CourseDto[0]), anonymousContext)).UserProfile;
            var confirmationSecret3 = emailService.RegisterSecrets.Last();
            Assert.IsNotNull(confirmationSecret3, "Confirmation email has to be sent.");


            // TEST: Cannot confirm the email with any of the old secrets
            await Assert.ThrowsExceptionAsync<RequestException>(() =>
                confirmEmailServerAction.ExecuteActionAsync(new ConfirmEmailRequestDto(confirmationSecret1),
                    anonymousContext));
            await Assert.ThrowsExceptionAsync<RequestException>(() =>
                confirmEmailServerAction.ExecuteActionAsync(new ConfirmEmailRequestDto(confirmationSecret2),
                    anonymousContext));

            // TEST: Can confirm the email with the newest confirmation secret
            var confirmedEmailResponse = await confirmEmailServerAction.ExecuteActionAsync(new ConfirmEmailRequestDto(confirmationSecret3),
                    anonymousContext);

            // TEST: The user should be automatically logged in after the first confirmation
            Assert.IsNotNull(confirmedEmailResponse.LoginToken);
            var user1Login1 = confirmedEmailResponse.LoginToken;

            // Create authentication context for the user's requests
            var (loginTokenModel, authenticationLevel) = authenticationService.TryAuthenticate(user1Login1.PrimaryTokenId);
            var user1Context = new RequestContext(loginTokenModel, authenticationLevel, new Guid().ToString(), Language.English, new System.Net.IPAddress(0x2414188f));

            // TEST: The user should be able to confirm the email again, but should not get logged in
            var confirmedEmailResponseAgain = await confirmEmailServerAction.ExecuteActionAsync(new ConfirmEmailRequestDto(confirmationSecret3),
                anonymousContext);
            Assert.IsNull(confirmedEmailResponseAgain.LoginToken);

            // TEST: The user should be able to log in manually
            var loginResponse = await loginServerAction.ExecuteActionAsync(new LoginRequestDto(user1Email, user1Password, new CourseDto[0]), anonymousContext);
            var manualLogin = loginResponse.LoginToken;

            // TODO: TEST: The user should be able to log out

            // TODO: TEST: The user should not be able to use the logged out token anymore

            // TEST: The user should be able to change the password
            await changePasswordServerAction.ExecuteActionAsync(
                new ChangePasswordRequestDto(user1Password, user1NewPassword, user1NewPassword), user1Context);

            // TEST: The user should not be able to change the password if a bad old password is provided
            await Assert.ThrowsExceptionAsync<RequestException>(() => changePasswordServerAction.ExecuteActionAsync(
                new ChangePasswordRequestDto("WrongOldPassword", user1NewPassword, user1NewPassword), user1Context));

            // TEST: The user shouldnt be able to register with the used email
            await Assert.ThrowsExceptionAsync<RequestException>(() => registerServerAction.ExecuteActionAsync(new RegisterRequestDto(user1Email, "Somsse Testussser", user1Password, user1Password, true, null, new CourseDto[0]), anonymousContext));

            // TODO: TEST: The user token should be able to extend its expiration automatically

            // TEST: The user token should be invalid after the expiration date
            // COMMENTED: Because we set artificially the token to expire in 3 years or so
            //timeService.MoveTime(Constants.LoginTokenLife.Add(TimeSpan.FromSeconds(5)));
            //var (token2, authenticationLevel2) = authenticationService.TryAuthenticate(user1Context.LoginToken.PrimaryTokenId);
            //Assert.IsNull(token2);
            //Assert.AreEqual(AuthenticationLevel.None, authenticationLevel2);
        }
    }
}

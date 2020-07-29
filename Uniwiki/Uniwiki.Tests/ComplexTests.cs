using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Server.Appliaction.ServerActions;
using Shared.Dtos;
using Shared.Exceptions;
using Shared.RequestResponse;
using Shared.Services.Abstractions;
using Shared.Tests;
using Uniwiki.Client.Host;
using Uniwiki.Client.Host.Components.FileUploader;
using Uniwiki.Client.Host.Components.SearchBox;
using Uniwiki.Client.Host.Pages;
using Uniwiki.Client.Host.Pages.Authorization;
using Uniwiki.Client.Host.Services;
using Uniwiki.Client.Host.Services.Abstractions;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Host;
using Uniwiki.Server.Host.Mvc;
using Uniwiki.Server.Host.Services.Abstractions;
using Uniwiki.Shared;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse.Authentication;
using Uniwiki.Shared.Tests.FakeServices;
using Uniwiki.Tests.Extensions;
using ILoginService = Uniwiki.Client.Host.Services.Abstractions.ILoginService;
using Program = Uniwiki.Client.Host.Program;
using TextService = Uniwiki.Client.Host.Services.TextService;

namespace Uniwiki.Tests
{
    [TestClass]
    public class ComplexTests
    {
        private FakeEmailService _emailService;
        private FakeTimeService _timeService;

        static ComplexTests()
        {
           Program.IsTest = true;
        }

        private ServiceProvider SetupDefaultDependencies()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddUniwikiClientHostServices();
            services.AddHostServices();
            services.AddSingleton<IHostingEnvironment, FakeHostingEnvironment>();

            services.AddScoped<IEmailService, FakeEmailService>();
            services.AddSingleton<ITimeService, FakeTimeService>();
            
            // _timeService = new FakeTimeService(new LikeTime(2020, 2, 27));

            // Arrange client
            services.AddScoped<IHttpService, FakeHttpService>();
            services.AddSingleton<IToastService, FakeToastService>();
            services.AddSingleton<INavigationService, FakeNavigationService>();
            services.AddSingleton<ILocalStorageService, FakeLocalStorageService>();
            services.AddSingleton<IJsInteropService, FakeJsInteropService>();


            var provider = services.BuildServiceProvider();

            _emailService = (FakeEmailService)provider.GetService<IEmailService>();
            _timeService = (FakeTimeService) provider.GetService<ITimeService>();
            _timeService.SetNow(new DateTime(2020, 4, 12, 4, 13, 44));

            return provider;
        }

        [TestMethod]
        public async Task Register_Login_ChangePassword_Logout_ForgottenPassword_Login_ShouldBeSuccessful()
        {
            // --- Arrange ---
            var provider = SetupDefaultDependencies();
            
            // Arrange data for testing
            var email = "petr.svetr@gmail.com";
            var password = "password123";
            var newPassword = "dogs987";
            var newNewPassword = "cats987";
            var name = "petr";
            var surename = "sindelar";

            // --- Act ---
            var registerRequestDto = new RegisterRequestDto(email, name + " " + surename, password, password, true);
            var registerPage = CreateRegisterPage(provider, registerRequestDto);
            await registerPage.Register();
            var registerSecret = _emailService.RegisterSecrets[0];

            var confirmEmailPage = CreateEmailConfirmedPage(provider, registerSecret.ToString(), email);
            await confirmEmailPage.ConfirmEmail();

            var loginRequestDto = new LoginRequestDto(email, password, new CourseDto[0]); // REAL
            var loginPage = CreateLoginPage(provider, loginRequestDto); // REAL
            await loginPage.Login();

            var passwordRequestDto = new ChangePasswordRequestDto(password, newPassword, newPassword);
            var changePasswordPage = CreateChangePasswordPage(provider, passwordRequestDto);
            await changePasswordPage.ChangePassword();

            var loginService = provider.GetService<ILoginService>();
            var profilePage = CreateProfilePage(provider, loginService.User.NameIdentifier);
            await profilePage.Logout();

            var restorePasswordPageForm = new RestorePasswordRequestDto(email);
            var restorePasswordPage = CreateRestorePasswordPage(provider, restorePasswordPageForm);
            await restorePasswordPage.RestorePassword();
            var restorePasswordSecret = _emailService.RestorePasswordSecrets[0];

            var newPasswordRequestDto = new CreateNewPasswordRequestDto(newNewPassword, restorePasswordSecret, newNewPassword);
            var createNewPasswordPage = CreateCreateNewPasswordPage(provider, newPasswordRequestDto, restorePasswordSecret.ToString());
            await createNewPasswordPage.CreateNewPassword();
            
            loginRequestDto.Password = newNewPassword;
            loginPage = CreateLoginPage(provider, loginRequestDto);
            await loginPage.Login();

            // --- Assert ---
            // There should be no exception thrown
        }

        [TestMethod]
        public async Task TestNegativeScenarios()
        {
            // --- Arrange ---
            var provider = SetupDefaultDependencies();

            // Arrange data for testing
            var email1 = "petr.svetr@gmail.com";
            var password1 = "password123";
            var newPassword1 = "dogs987";
            var newNewPassword1 = "cats987";
            var name1 = "petr";
            var surename1 = "sindelar";

            var email2 = "marek.koko@gmail.com";
            var password2 = "porjjjjd123";
            var newPassword2 = "dogskoko987";
            var newNewPassword2 = "catkokos987";
            var name2 = "marek";
            var surename2 = "koko";

            var email3 = "zirafa@gmail.com";
            var password3 = "obecnazirafa";
            var newPassword3 = "slonak987";
            var newNewPassword3 = "zirafafa987";
            var name3 = "zirafa";
            var surename3 = "obecna";

            var someSecret = Guid.Parse("b6be6d12-4d7d-4cb0-a59c-fc5c0c2179e4");

            // --- Act ---
            // Try login without registration
            await Assert.ThrowsExceptionAsync<RequestException>(() => CreateLoginPage(provider, new LoginRequestDto(email1, password1, new CourseDto[0])).Login());
            // Try to validate email without registration
            await Assert.ThrowsExceptionAsync<RequestException>(() => CreateEmailConfirmedPage(provider, someSecret.ToString(), email1).ConfirmEmail());
            // Try to change password without login
            await Assert.ThrowsExceptionAsync<RequestException>(() => CreateChangePasswordPage(provider, new ChangePasswordRequestDto(password1, "sss", "sss")).ChangePassword());
            // Try to restore password of non registered user
            await Assert.ThrowsExceptionAsync<RequestException>(() => CreateRestorePasswordPage(provider, new RestorePasswordRequestDto(email1)).RestorePassword());
            // Create new password of non registered user
            await Assert.ThrowsExceptionAsync<RequestException>(() => CreateCreateNewPasswordPage(provider, new CreateNewPasswordRequestDto("www", Guid.Empty, "wwww"),someSecret.ToString()).CreateNewPassword());

            // Register user 1
            await CreateRegisterPage(provider, new RegisterRequestDto(email1, name1 + " " + surename1, password1, password1, true)).Register();
            var registerSecret1 = _emailService.RegisterSecrets.Last();

            // Try login without confirming the email
            await Assert.ThrowsExceptionAsync<RequestException>(() => CreateLoginPage(provider, new LoginRequestDto(email1, password1, new CourseDto[0])).Login());
            // Try login without registration
            await Assert.ThrowsExceptionAsync<RequestException>(() => CreateLoginPage(provider, new LoginRequestDto(email2, password2, new CourseDto[0])).Login());
            // Try to validate email with wrong secret
            await Assert.ThrowsExceptionAsync<RequestException>(() => CreateEmailConfirmedPage(provider, someSecret.ToString(), email2).ConfirmEmail());
            // Try to change password without login
            await Assert.ThrowsExceptionAsync<RequestException>(() => CreateChangePasswordPage(provider, new ChangePasswordRequestDto(password1, "sss", "sss")).ChangePassword());
            // Try to restore password of non registered user
            await Assert.ThrowsExceptionAsync<RequestException>(() => CreateRestorePasswordPage(provider, new RestorePasswordRequestDto(email2)).RestorePassword());
            // Create new password of non registered user
            await Assert.ThrowsExceptionAsync<RequestException>(() => CreateCreateNewPasswordPage(provider, new CreateNewPasswordRequestDto("www", Guid.NewGuid(), "wwww"), someSecret.ToString()).CreateNewPassword());

            // Register user 1 once more (before he confirms the email, before its time for resending the email)
            await Assert.ThrowsExceptionAsync<RequestException>(() => CreateRegisterPage(provider, new RegisterRequestDto(email1, name1 + " " + surename1, password1, password1, true)).Register());

            // Move time
            _timeService.SetNow(_timeService.Now.Add(Constants.ResendRegistrationEmailMinTime.Add(TimeSpan.FromSeconds(5))));

            // Try to register again
            await CreateRegisterPage(provider, new RegisterRequestDto(email1, name1 + " " + surename1, password1, password1, true)).Register();
            var registerSecret1b = _emailService.RegisterSecrets.Last();

            // Try to confirm the email of user 1 with his old confirmation email
            await Assert.ThrowsExceptionAsync<RequestException>(() => CreateEmailConfirmedPage(provider, registerSecret1.ToString(), email1).ConfirmEmail());
            
             // Confirm the email of user 1 with the new confirmation email
            await CreateEmailConfirmedPage(provider, registerSecret1b.ToString(), email1).ConfirmEmail();

            // Try to register the user
            await Assert.ThrowsExceptionAsync<RequestException>(() => CreateRegisterPage(provider, new RegisterRequestDto(email1, name1 + " " + surename1, password1, password1, true)).Register());

            // Login the user 1
            await CreateLoginPage(provider, new LoginRequestDto(email1, password1, new CourseDto[0])).Login();

            // Change the password of user 1
            await CreateChangePasswordPage(provider, new ChangePasswordRequestDto(password1, newPassword1, newPassword1)).ChangePassword();

            var loginService = provider.GetService<ILoginService>();
            // Logout
            await CreateProfilePage(provider, loginService.User.NameIdentifier).Logout();

            // Restore password
            await CreateRestorePasswordPage(provider, new RestorePasswordRequestDto(email1)).RestorePassword();
            var restorePasswordSecret = _emailService.RestorePasswordSecrets.Last();

            // Create new password
            var createNewPasswordPageForm = new CreateNewPasswordRequestDto(newNewPassword1, restorePasswordSecret, newNewPassword1);
            var createNewPasswordPage = CreateCreateNewPasswordPage(provider, createNewPasswordPageForm, restorePasswordSecret.ToString());
            await createNewPasswordPage.CreateNewPassword();

            // Login using the new password
            await CreateLoginPage(provider, new LoginRequestDto(email1, newNewPassword1, new CourseDto[0])).Login();
        }

        [TestMethod]
        public void CanInjectDependencies()
        {
            var provider = SetupDefaultDependencies();
            
            var objectWithInjects = new FakeObjectWithInjects();

            provider.InjectDependencies(objectWithInjects);

            Assert.IsTrue(objectWithInjects.HasPublicDependency(), "The object did not receive the public dependency");
            Assert.IsTrue(objectWithInjects.HasPrivateDependency(), "The object did not receive the private dependency");
        }

        private CreateNewPasswordPage CreateCreateNewPasswordPage(ServiceProvider provider, CreateNewPasswordRequestDto request, string secret)
        {
            var page = new CreateNewPasswordPage();
            provider.InjectDependencies(page);
            page.Request = request;
            page.Secret = secret;
            return page;
        }

        private RegisterPage CreateRegisterPage(ServiceProvider provider, RegisterRequestDto request)
        {
            var page = new RegisterPage();
            provider.InjectDependencies(page);
            page.Request = request;
            return page;
        }

        private EmailConfirmedPage CreateEmailConfirmedPage(ServiceProvider provider, string secret, string email)
        {
            var page = new EmailConfirmedPage();
            provider.InjectDependencies(page);
            page.Secret = secret;
            page.Email = email;
            return page;
        }

        private LoginPage CreateLoginPage(ServiceProvider provider, LoginRequestDto request)
        {
            var page = new LoginPage();
            provider.InjectDependencies(page);
            page.Request = request;
            return page;
        }

        private ChangePasswordPage CreateChangePasswordPage(ServiceProvider provider, ChangePasswordRequestDto request)
        {
            var page = new ChangePasswordPage();
            provider.InjectDependencies(page);
            page.Request = request;
            return page;
        }

        private ProfilePage CreateProfilePage(ServiceProvider provider, string url)
        {
            var page = new ProfilePage();
            provider.InjectDependencies(page);
            page.Url = url;
            return page;
        }

        private RestorePasswordPage CreateRestorePasswordPage(ServiceProvider provider, RestorePasswordRequestDto request)
        {
            var page = new RestorePasswordPage();
            provider.InjectDependencies(page);
            page.Request = request;
            return page;
        }
    }

    class FakeHttpService : IHttpService
    {
        private readonly IMvcProcessor _mvcProcessor;
        private readonly IRequestDeserializer _requestDeserializer;

        public FakeHttpService(IMvcProcessor mvcProcessor, IRequestDeserializer requestDeserializer)
        {
            _mvcProcessor = mvcProcessor;
            _requestDeserializer = requestDeserializer;
        }

        public async Task<HttpResponseMessage> PostAsync(string apiServer, HttpContent data)
        {
            // Read the provided data
            var stringData = await data.ReadAsStringAsync();

            // Deserialize the given data
            var dataForServer = JsonConvert.DeserializeObject<DataForServer>(stringData);

            var inputContext = new InputContext(dataForServer.AccessToken, Guid.NewGuid().ToString(), dataForServer.Language, ClientConstants.AppVersionString, new System.Net.IPAddress(0x2414188f));

            var request = _requestDeserializer.Deserialize(dataForServer.Request, dataForServer.Type);

            // Push it to the MVC module
            var response = await _mvcProcessor.Process(request, inputContext);

            // Serialize response (to simulate real reciving of a request)
            var serializedResponse = JsonConvert.SerializeObject(response);

            // Create content for http response
            var stringContent = new StringContent(serializedResponse);

            // Create http response to return
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = stringContent };

            return httpResponse;
        }
    }

    class FakeNavigationService : INavigationService
    {
        public FakeNavigationService(List<string> navigationStack = null)
        {
            NavigationStack = navigationStack ?? new List<string>();
        }

        public List<string> NavigationStack { get; }

        public void NavigateTo(string url, bool forceLoad = false)
        {
            NavigationStack.Add(url);
        }

        public Task Back()
        {
            if(NavigationStack.Any())
                NavigationStack.RemoveAt(NavigationStack.Count - 1);

            return Task.CompletedTask;
        }

        public void NavigateToTheCurrentUrl()
        {
            
        }

        public void ForceReload()
        {
            
        }

        public string CurrentUrl => NavigationStack.Last();
    }

    class FakeServerActionResolver : IServerActionResolver
    {
        private readonly IServerAction[] _serverActions;
        private readonly Dictionary<Type, IServerAction> _requestsAndServerActions;

        public FakeServerActionResolver(params IServerAction[] serverActions)
        {
            _serverActions = serverActions;
            _requestsAndServerActions = new Dictionary<Type, IServerAction>();
            foreach (var serverAction in serverActions)
            {
                var type = serverAction.GetType();
                var baseType = type.BaseType;

                if(baseType == null)
                    Assert.Fail("The serverAction with type " + type.FullName + " does not have a baseType");

                var genericArguments = baseType.GetGenericArguments();

                if(genericArguments.Length == 1)
                    Assert.Fail("The serverAction with type " + type.FullName + " does not have one generic argument.");

                _requestsAndServerActions.Add(genericArguments[0], serverAction);
            }
        }

        public IServerAction Resolve(IRequest request)
        {
            var requestType = request.GetType();
            var serverAction = _requestsAndServerActions[requestType];
            return serverAction;
        }
    }

    class FakeToastService : IToastService
    {
        event Action<ToastLevel, RenderFragment, string> IToastService.OnShow
        {
            add
            {
                
            }

            remove
            {
                
            }
        }

        public void ShowInfo(string message, string heading = "")
        {
            
        }

        public void ShowSuccess(string message, string heading = "")
        {
            
        }

        public void ShowWarning(string message, string heading = "")
        {
            
        }

        public void ShowError(string message, string heading = "")
        {
            
        }

        public void ShowToast(ToastLevel level, string message, string heading = "")
        {
            
        }

        public void ShowInfo(RenderFragment message, string heading = "")
        {
            throw new NotImplementedException();
        }

        public void ShowSuccess(RenderFragment message, string heading = "")
        {
            throw new NotImplementedException();
        }

        public void ShowWarning(RenderFragment message, string heading = "")
        {
            throw new NotImplementedException();
        }

        public void ShowError(RenderFragment message, string heading = "")
        {
            throw new NotImplementedException();
        }

        public void ShowToast(ToastLevel level, RenderFragment message, string heading = "")
        {
            throw new NotImplementedException();
        }

        public event Action<ToastLevel, string, string> OnShow;
    }

    class FakeTimeService : ITimeService
    {
        public DateTime Now { get; private set; }

        public FakeTimeService()
        {
            
        }

        public void SetNow(DateTime newTime)
        {
            Now = newTime;
        }

    }

    class FakePeriodicalTimer : IPeriodicalTimer
    {
        public int PeriodsLeft { get; set; }
        public bool IsRunning => PeriodsLeft > 0;

        public void Start(TimeSpan period, int periods, Action periodElapsed)
        {
            
        }

        public void Stop()
        {
            
        }
    }

    class FakeLocalStorageService : ILocalStorageService
    {
        private readonly Dictionary<string, object> _storage;
        public FakeLocalStorageService()
        {
            _storage = new Dictionary<string, object>();
        }

        public Task ClearAsync()
        {
            _storage.Clear();

            return Task.CompletedTask;
        }

        public Task<T> GetItemAsync<T>(string key)
        {
            return Task.FromResult((T) _storage[key]);
        }

        public Task<string> KeyAsync(int index)
        {
            return Task.FromResult(_storage.Keys.ElementAt(index));
        }

        public Task<bool> ContainKeyAsync(string key)
        {
            return Task.FromResult(_storage.ContainsKey(key));

        }

        public Task<int> LengthAsync()
        {
            return Task.FromResult(_storage.Count);
        }

        public Task RemoveItemAsync(string key)
        {
            _storage.Remove(key);

            return Task.CompletedTask;
        }

        public Task SetItemAsync<T>(string key, T data)
        {
            _storage[key] = data;

            return Task.CompletedTask;
        }

        public event EventHandler<ChangingEventArgs> Changing;
        public event EventHandler<ChangedEventArgs> Changed;
    }

    public class FakeJsRuntime : IJSRuntime
    {
        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object[] args)
        {
            return new ValueTask<TValue>(default(TValue));
        }

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object[] args)
        {
            return new ValueTask<TValue>(default(TValue));
        }
    }

    public class FakeJsInteropService : IJsInteropService
    {
        public ValueTask FocusElementAsync(ElementReference element)
        {
            return new ValueTask();
        }

        public ValueTask HideCollapse(string elementId)
        {
            return new ValueTask();
        }

        public ValueTask<bool> NavigationBack()
        {
            return new ValueTask<bool>(true);
        }

        public ValueTask<string> GetBrowserLanguage()
        {
            return new ValueTask<string>("en");
        }

        public ValueTask ScrollIntoView(string elementId)
        {
            return new ValueTask();
        }

        public ValueTask SetScrollCallback(DotNetObjectReference<ScrollService> netRef)
        {
            return new ValueTask();
        }

        public ValueTask SetHeightToInitial(ElementReference element)
        {
            return new ValueTask();
        }

        public ValueTask MyInputInit(ElementReference? fileInput, DotNetObjectReference<InputFileCallbacks> callbacksAsNetRef)
        {
            return new ValueTask();
        }

        public ValueTask StartFileUpload(in int id, string dataForServer)
        {
            return new ValueTask();
        }

        public ValueTask AbortFileUpload(in int id)
        {
            return new ValueTask();
        }

        public ValueTask Download(in string data, in string fileName)
        {
            return new ValueTask();
        }

        public ValueTask StartFileUpload(in string id, string dataForServer)
        {
            return new ValueTask();
        }

        public ValueTask AbortFileUpload(in string id)
        {
            return new ValueTask();
        }
    }

    public class FakeObjectWithInjects
    {
        [Inject] private IRequestSender RequestSender { get; set; }
        [Inject] public ITimeService TimeService { get; set; }

        public bool HasPrivateDependency() => RequestSender != null;
        public bool HasPublicDependency() => TimeService != null;
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Server.Appliaction.Services.Abstractions;
using Shared;
using Uniwiki.Server.Application.Services;

namespace Uniwiki.Server.Application.Tests
{
    [TestClass]
    public class EmailTemplateServiceTests
    {
        // Vizual check in the console!
        [TestMethod]
        public void RegistrationEmailCanBeCreated()
        {
            var uploadFileService = new FakeUploadFileService();
            var textService = new TextService();
            textService.SetLanguage(Language.Czech); // Set language of the email
            var emailTemplateService = new EmailTemplateService(uploadFileService, textService);
            var confirmationLink = "https://uniwiki.com/some/confi9438729874";
            var registerEmailTemplate = emailTemplateService.GetVerifyEmailText(confirmationLink);
            Console.WriteLine(registerEmailTemplate);
        }

        // Vizual check in the console!
        [TestMethod]
        public void RestorePasswordEmailCanBeCreated()
        {
            var uploadFileService = new FakeUploadFileService();
            var textService = new TextService();
            textService.SetLanguage(Language.Czech); // Set language of the email
            var emailTemplateService = new EmailTemplateService(uploadFileService, textService);
            var confirmationLink = "https://uniwiki.com/some/confi9438729874";
            var registerEmailTemplate = emailTemplateService.GetRestorePasswordText(confirmationLink);
            Console.WriteLine(registerEmailTemplate);
        }
    }

    class FakeUploadFileService : IUploadFileService
    {
        public string PostFilesDirectoryPath => throw new NotImplementedException();

        public string EmailTemplatesDirectoryPath => Path.Combine(typeof(UniwikiServerApplicationServices).Assembly.Location, "EmailTemplates");

        public IFormFile GetFile()
        {
            throw new NotImplementedException();
        }

        public void RegisterFile(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}

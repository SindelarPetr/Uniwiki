using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Server.Appliaction.Services.Abstractions;
using Shared;
using Uniwiki.Server.Application.Services;
using Uniwiki.Shared;
using System.Web;
using Uniwiki.Shared.Services.Abstractions;
using Uniwiki.Shared.Tests.FakeServices;

namespace Uniwiki.Server.Application.Tests
{
    [TestClass]
    public class EmailTemplateServiceTests
    {
        [TestMethod]
        public void CanFindEmailTemplates()
        {
            var uploadFileService = new FakeUploadFileService();
            Assert.IsTrue(Directory.Exists(uploadFileService.EmailTemplatesDirectoryPath));
        }

        // Vizual check in the console!
        [TestMethod]
        public void RegistrationEmailCanBeCreated()
        {
            var uploadFileService = new FakeUploadFileService();
            var textService = new TextService(new FakeLanguageService());
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
            var textService = new TextService(new FakeLanguageService());
            var emailTemplateService = new EmailTemplateService(uploadFileService, textService);
            var confirmationLink = "https://uniwiki.com/some/confi9438729874";
            var registerEmailTemplate = emailTemplateService.GetRestorePasswordText(confirmationLink);
            Console.WriteLine(registerEmailTemplate);
        }
    }

    class FakeUploadFileService : IUploadFileService
    {
        public string PostFilesDirectoryPath => throw new NotImplementedException();

        public string EmailTemplatesDirectoryPath
        {
            get
            {
                var root = Path.GetDirectoryName(typeof(UniwikiServerApplicationServices).Assembly.Location);
                return Path.Combine(root, "EmailTemplates");
            }
        }

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

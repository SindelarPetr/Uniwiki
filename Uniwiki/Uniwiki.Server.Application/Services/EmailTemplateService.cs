using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Server.Appliaction.Services.Abstractions;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Shared;

namespace Uniwiki.Server.Application.Services
{
    internal class EmailTemplateService : IEmailTemplateService
    {
        private readonly IUploadFileService _uploadFileService;
        private readonly TextService _textService;

        public EmailTemplateService(IUploadFileService uploadFileService, TextService textService)
        {
            _uploadFileService = uploadFileService;
            _textService = textService;
        }

        public string GetVerifyEmailText(string confirmationLink)
        {
            // Load the template from the file
            var path = _uploadFileService.EmailTemplatesDirectoryPath;
            var template = File.ReadAllText(Path.Combine(path, "RegistrationEmail.html"));

            var data = new Dictionary<string, string>()
            {
                {"title", _textService.EmailVerifyRegistration_Title },
                {"preheader", _textService.EmailVerifyRegistration_Preheader },
                {"header", _textService.EmailVerifyRegistration_Header },
                {"addressing", _textService.EmailVerifyRegistration_Addressing },
                {"text", _textService.EmailVerifyRegistration_Text },
                {"buttonConfirmEmail", _textService.EmailVerifyRegistration_ButtonConfirmEmail },
                {"displayingProblems", _textService.EmailVerifyRegistration_DisplayingProblems },
                {"contactUs", _textService.EmailVerifyRegistration_ContactUs },
                {"facebookLink", Constants.FacebookLink },
                {"confirmationLink", confirmationLink }
            };

            return ProcessTemplate(template, data);
        }

        public string GetRestorePasswordText(string restoreLink)
        {
            var path = _uploadFileService.EmailTemplatesDirectoryPath;
            var template = File.ReadAllText(Path.Combine(path, "RestorePasswordEmail.html"));

            var data = new Dictionary<string, string>()
            {
                {"title", _textService.EmailRestorePassword_Title },
                {"preheader", _textService.EmailRestorePassword_Preheader },
                {"header", _textService.EmailRestorePassword_Header },
                {"text", _textService.EmailRestorePassword_Text },
                {"buttonRestorePassword", _textService.EmailRestorePassword_ButtonRestorePassword },
                {"displayingProblems", _textService.EmailRestorePassword_DisplayingProblems },
                {"contactUs", _textService.EmailRestorePassword_ContactUs },
                {"facebookLink", Constants.FacebookLink },
                {"restoreLink", restoreLink }
            };

            return ProcessTemplate(template, data);
        }

        private static string ProcessTemplate(string template, Dictionary<string, string> data)
        {
            return Regex.Replace(template, "\\{(.*?)\\}", m =>
            m.Groups.Count > 1 && data.ContainsKey(m.Groups[1].Value) ?
            data[m.Groups[1].Value] : throw new KeyNotFoundException($"The variable '{m.Value}' was not present in the given data dictionary."));
        }
    }
}

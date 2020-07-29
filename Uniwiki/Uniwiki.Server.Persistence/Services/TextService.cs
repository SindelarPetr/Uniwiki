using System;
using System.Linq;
using Uniwiki.Shared.Services;

namespace Uniwiki.Server.Persistence.Services
{
    // We dont want to abstract this one - translating would then be too much time consuming
    internal class TextService
    {
        private readonly TextServiceBase _textServiceBase;

        public TextService(TextServiceBase textServiceBase)
        {
            _textServiceBase = textServiceBase;
        }

        public string Error_CourseNotFound => _textServiceBase.GetTranslation("Předmět nenalezen.", "Course not found.");
        public string Error_EmailConfirmationFailed => _textServiceBase.GetTranslation("Potvrzení emailu selhalo.", "Email confirmation failed.");
        public string Error_FailedToCreateTheNewPassword => _textServiceBase.GetTranslation("Nepodařilo se vytvořit nové heslo.", "Failed to create the new password.");
        public string Error_UserNotFound => _textServiceBase.GetTranslation("Uživatel nebyl nalezen.", "User has not been found.");
        public string Error_UniversityNotFound =>
            _textServiceBase.GetTranslation("Univerzita nebyla nalezena.", "University was not found.");

        public string Error_PostNotFound => _textServiceBase.GetTranslation("Nepodařilo se nalézt příspěvek.", "Was not able to find the desired post.");

        public string Error_LanguageNotSupported =>
            _textServiceBase.GetTranslation("Tento jazyk není podporovaný.", "The provided language is not supported.");

        public string Error_CouldNotFindFile(string fileName) => _textServiceBase.GetTranslation($"Nepodařilo se nalézt soubor {fileName}, zkuste jej nahrát znovu.", $"Failed to find file {fileName}. Try to upload it again.");

        public string Error_EmailIsAlreadyTaken(string email) =>
            _textServiceBase.GetTranslation($"Email {email} je již zabrán.", $"The email {email} is already taken.");

        public string Error_NoUserWithProvidedEmail(string email) => _textServiceBase.GetTranslation(
            $"Uživatel s emailem {email} nebyl nalezen.", $"User with the email {email} has not been found.");

        public string Error_FilesNotFound(string[] notFoundNames)
        {
            if (!notFoundNames.Any())
                throw new ArgumentException("There must be at least one name of a not found file.", nameof(notFoundNames));

            // Get file names in format: "file1, file2, file3"
            var names = notFoundNames.Aggregate((a, b) => $"{a}, {b}");

            if (names.Length == 0)
                return _textServiceBase.GetTranslation($"Nepodařilo se nalézt soubor: {names}. Zkuste jej odebrat a nahrát znovu.",
                    $"Could not upload file: {names}. Try to upload it again.");

            return _textServiceBase.GetTranslation($"Nepodařilo se nalézt soubory: {names}. Zkuste je odebrat a nahrát znovu.",
                $"Could not upload files: {names}. Try to remove them and upload them again.");

        }
    }
}

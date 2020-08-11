using System;
using System.Linq;
using Shared.Exceptions;
using Uniwiki.Shared.Services;

namespace Uniwiki.Server.Persistence.Services
{
    // We dont want to abstract this one - translating would then be too much time consuming
    internal class TextService
    {
        private readonly ILanguageService _languageService;

        public TextService(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public string Error_CourseNotFound => _languageService.GetTranslation("Předmět nenalezen.", "Course not found.");

        public string Error_PostCommentLikeNotFound => _languageService.GetTranslation("Tento like nebyl nalezen.", "The like has not been found.");

        public string Error_EmailConfirmationFailed => _languageService.GetTranslation("Potvrzení emailu selhalo.", "Email confirmation failed.");

        public string Error_FailedToCreateTheNewPassword => _languageService.GetTranslation("Nepodařilo se vytvořit nové heslo.", "Failed to create the new password.");

        public string Error_UserNotFound => _languageService.GetTranslation("Uživatel nebyl nalezen.", "User has not been found.");
        public string Error_UniversityNotFound =>
            _languageService.GetTranslation("Univerzita nebyla nalezena.", "University was not found.");

        public string Error_PostNotFound => _languageService.GetTranslation("Nepodařilo se nalézt příspěvek.", "Was not able to find the desired post.");

        public string Error_LanguageNotSupported =>
            _languageService.GetTranslation("Tento jazyk není podporovaný.", "The provided language is not supported.");

        public string Error_CourseVisitNotFound
            => _languageService.GetTranslation("Návštěva kurzu nenalezena.", "The course visit has not been found.");

        public string EmailConfirmationSecretNotFound
                => _languageService.GetTranslation("Dané potvrzovací tajemství nebylo nalezeno.", "The confirmation secret has not been found.");

        public string Error_FeedbackNotFound 
            => _languageService.GetTranslation("Tato zpětná vazba nebyla nalezena.", "The feedback has not been found.");

        public string Error_LoginTokenNotFound => _languageService.GetTranslation("Tento přihlašovací token nebyl nalezen.", "The login token has not been found.");

        public string Error_NewPasswordSecretNotFound => _languageService.GetTranslation("Toto tajemství pro tvorbu hesla nebylo nalezeno.", "This new password secret has not been found.");

        public string Error_PostCommentNotFound => _languageService.GetTranslation("Tento komentář nebyl nalezen.", "The comment has not been found.");

        public string Error_PostFileDownloadNotFound => _languageService.GetTranslation("Toto stažení souboru nebylo nalezeno.", "The file download has not been found.");

        public string PostFileNotFound => _languageService.GetTranslation("Tento soubor u nás bohužel nemáme.", "The file has not been found.");

        public string Error_PostLikeNotFound => _languageService.GetTranslation("Tento like nebyl nalezen.", "The like has not been found.");

        public string Error_ProfileNotFound => _languageService.GetTranslation("Profil nebyl nalezen.", "The profile has not been found.");

        public string FacultyNotFound => _languageService.GetTranslation("Tato fakulta nebyla nalezena.", "The faculty has not been found.");

        public string Error_NoFacultyWithUrl(string facultyUrlName)
            => _languageService.GetTranslation($"Na adrese '{ facultyUrlName }' jsme nenašli žádnou fakultu", $"There is no faculty with the url '{ facultyUrlName }'");

        public string Error_CouldNotFindFile(string fileName) => _languageService.GetTranslation($"Nepodařilo se nalézt soubor {fileName}, zkuste jej nahrát znovu.", $"Failed to find file {fileName}. Try to upload it again.");

        public string Error_EmailIsAlreadyTaken(string email) =>
            _languageService.GetTranslation($"Email {email} je již zabrán.", $"The email {email} is already taken.");

        public string Error_NoUserWithProvidedEmail(string email) => _languageService.GetTranslation(
            $"Uživatel s emailem {email} nebyl nalezen.", $"User with the email {email} has not been found.");

        public string Error_FilesNotFound(string[] notFoundNames)
        {
            if (!notFoundNames.Any())
                throw new ArgumentException("There must be at least one name of a not found file.", nameof(notFoundNames));

            // Get file names in format: "file1, file2, file3"
            var names = notFoundNames.Aggregate((a, b) => $"{a}, {b}");

            if (names.Length == 0)
                return _languageService.GetTranslation($"Nepodařilo se nalézt soubor: {names}. Zkuste jej odebrat a nahrát znovu.",
                    $"Could not upload file: {names}. Try to upload it again.");

            return _languageService.GetTranslation($"Nepodařilo se nalézt soubory: {names}. Zkuste je odebrat a nahrát znovu.",
                $"Could not upload files: {names}. Try to remove them and upload them again.");

        }
    }
}

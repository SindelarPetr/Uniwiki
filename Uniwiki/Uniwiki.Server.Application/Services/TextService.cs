using System;
using Uniwiki.Shared;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Server.Application.Services
{
    // We dont want to abstract this one - translating would then be too much time consuming
    internal class TextService
    {
        private readonly ILanguageService _languageService;

        public TextService(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        internal string Error_PostNotFound => _languageService.GetTranslation("Příspěvek nebyl nalezen.", "The Post was not found.");

        public string Error_EmailConfirmationFailed => _languageService.GetTranslation("Nepodařilo se potvrdit email tímto odkazem.", "Confirmation of your email is not possible with this link.");

        public string Error_WaitBeforeRepeatedDownload => _languageService.GetTranslation($"Pro opětovné stažení souboru musíte počkat {Constants.DownloadAgainTime.Seconds} s.", $"You need to wait ({Constants.DownloadAgainTime.Seconds} s) before you will be able to download the file again.");
        public string Error_EmailHasWrongFormat => _languageService.GetTranslation("Zadaný email není platný.", "Provided email is not valid.");
        public string Email_RegisterSubject => _languageService.GetTranslation("Potvrďte Váš email", "Confirm your email");
        public string Email_RestorePasswordSubject => _languageService.GetTranslation("Tvorba nového hesla", "Create a new password");
        public string Error_CouldNotSendEmail(string email) => _languageService.GetTranslation($"Nepodařilo se odeslat email na adresu {email}", $"Couldnt send a mail to {email}");
        public string Error_BadAuthentication => _languageService.GetTranslation("Vyskytl se problém s přihlášením.", "There was a problem with log in.");

        public string Error_CouldNotRefreshPassword => _languageService.GetTranslation("Nepodařilo se obnovit heslo. Zkuste to znovu.",
            "Could not refresh the password. Try again.");

        internal string Error_UniversityNameOrUrlNotUniq(string name, string url) => _languageService.GetTranslation($"Buď název univerzity '{name}' nebo url '{url}' je již zabrané.", $"The name of the university {name} or url {url} is not uniq.");

        public string Error_OldPasswordsDontMatch =>
            _languageService.GetTranslation("Staré heslo není správné.", "The old password is not valid.");

        public string Error_WrongLoginCredentials =>
            _languageService.GetTranslation("Zadány neplatné přihlašovací údaje.", "Wrong email or password.");

        public string Error_EmailHasBeenAlreadySent => _languageService.GetTranslation(
            "Email byl již odeslán. Nezapomeňte zkontrolovat spam.",
            "Email has already been sent. Dont forget to check your spam.");

        public string Error_CouldNotEditPost => _languageService.GetTranslation(
            "Nepodařilo se upravit příspěvek.", "Could not edit the desired post.");

        public string EmailVerifyRegistration_Title => _languageService.GetTranslation("Potvrďte Váš email", "Confirm your email");

        public string EmailVerifyRegistration_Preheader => _languageService.GetTranslation("Potvrďte Váš email kliknutím na odkaz", "Confirm your email by clicking on the given link");

        public string EmailVerifyRegistration_Header => _languageService.GetTranslation("Potvrďte Váš email", "Confirm your email");

        public string EmailVerifyRegistration_Addressing => _languageService.GetTranslation("Dobrý den,", "Hello,");

        public string EmailVerifyRegistration_Text => _languageService.GetTranslation("Děkujeme, že jste se rozhodl/a stát se součástí Uniwiki. K dokončení již zbývá poslední krok, což je potvrzení emailu.", 
            "Thank you for becoming a member of Uniwiki. There is just one step left fo you, which is to confirm your email.");

        public string EmailVerifyRegistration_ButtonConfirmEmail => _languageService.GetTranslation("Potvrdit email", "Confirm email");

        public string EmailVerifyRegistration_DisplayingProblems => _languageService.GetTranslation("Pokud se Vám nezobrazuje email správně, můžete zkusit tento link",
            "If the email is not well displayed, you can try the following link.");

        public string EmailVerifyRegistration_ContactUs => _languageService.GetTranslation("Neváhejte nás kontaktovat přes", "Do not hesitate to contact us through");

        public string EmailRestorePassword_Title => _languageService.GetTranslation("Obnovit heslo", "Restore password");

        public string EmailRestorePassword_Preheader => _languageService.GetTranslation("Obnovte vaše heslo kliknutím na odkaz", "Restore your password by clicking the link");

        public string EmailRestorePassword_Header => _languageService.GetTranslation("Obnovit heslo", "Restore password");

        public string EmailRestorePassword_Text => _languageService.GetTranslation("Každému se to čas od času stane :-)", "It happens to everyone from time to time :-)");

        public string UploadPostFile(string fileName) => _languageService.GetTranslation($"Nemohli jsme uložit soubor { fileName }. Úložiště je pravděpodobně zaplněné. Doporučujeme vám kontaktovat nás na Facebooku.", $"Was not able to upload the file { fileName }, the storage on the server is probably full. We recommend you to contact the support.");

        public string EmailRestorePassword_ButtonRestorePassword => _languageService.GetTranslation("Vytvořit nové heslo", "Create a new password");

        public string EmailRestorePassword_DisplayingProblems => _languageService.GetTranslation("Pokud se Vám nezobrazuje email správně, můžete zkusit tento link", "If the email is not displayed properly, you can try clicking the following link ");

        public string EmailRestorePassword_ContactUs => _languageService.GetTranslation("Případně nás neváhejte kontaktovat přes", "Do not hesitate to contact us via");

        public string EditComment_YouCannotRemoveSomeoneElsesComment => _languageService.GetTranslation("Nemůžete odstranit cizí komentář!", "You cannot remove someone else's comment!");

        public string RemovePost_CannotRemoveNonOwnersPost => _languageService.GetTranslation("Nemůžete odstranit cizí komentář", "Its not possible to remove someone else's post.");

        public string Error_PostCommentNotFound => _languageService.GetTranslation("Daný komentář nebyl nalezen.", "The specified comment has not been found.");

        public string ResendConfirmation_ProfileIsAlreadyConfirmed(string email) => _languageService.GetTranslation(
            $"Email {email} byl již potvrzen.", 
            $"The email {email} was already confirmed.");

        public string Error_YourEmailWasNotYetConfirmed(string email) => _languageService.GetTranslation(
            $"Váš účet nebyl aktivován. Aktivujte jej přes odkaz v emailu {email}. Případně opakujte registraci.",
            $"Your email was not confirmed yet. Confirm it through a link in your email {email}. Register again eventually.");

        public string Error_EmailIsAlreadyUsed(string email) => _languageService.GetTranslation($"Email {email} je již používán.",
            $"The email {email} is already used.");

        public string Email_RestorePasswordMessage(string link) => _languageService.GetTranslation(
            $"<a href=\"{link}\">Nastavit nové heslo</a>. Nebo klikněte na tento link {link}",
            $"<a href=\"{link}\">Create a new password</a>. Or click the following link {link}");

        public string Email_RegisterMessage(string link) => _languageService.GetTranslation(
            $"<a href=\"{link}\">Potvrdit email</a><p>Případně klikněte na tento odkaz: {link}</p>",
            $"<a href=\"{link}\">Confirm the email</a><p> Or click the following link: {link}</p>");

        public string Error_CourseNameTaken(string name) => _languageService.GetTranslation($"Předmět s názvem '{name}' již existuje.", $"Course '{name}' already exists.");

        public string Error_StudyGroupNameIsTaken(string name) => _languageService.GetTranslation(
            $"Studijní skupina s názvem '{name}' již existuje.", $"Study group with the name '{name}' already exists.");
    }
}

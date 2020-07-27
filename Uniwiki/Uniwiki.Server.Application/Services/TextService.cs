using System;
using Uniwiki.Shared;
using Uniwiki.Shared.Services;

namespace Uniwiki.Server.Application.Services
{
    // We dont want to abstract this one - translating would then be too much time consuming
    internal class TextService : TextServiceBase
    {
        public string Error_EmailConfirmationFailed => GetTranslation("Nepodařilo se potvrdit email tímto odkazem.", "Confirmation of your email is not possible with this link.");

        public string Error_WaitBeforeRepeatedDownload => GetTranslation($"Pro opětovné stažení souboru musíte počkat {Constants.DownloadAgainTime.Seconds} s.", $"You need to wait ({Constants.DownloadAgainTime.Seconds} s) before you will be able to download the file again.");
        public string Error_EmailHasWrongFormat => GetTranslation("Zadaný email není platný.", "Provided email is not valid.");
        public string Email_RegisterSubject => GetTranslation("Potvrďte Váš email", "Confirm your email");
        public string Email_RestorePasswordSubject => GetTranslation("Tvorba nového hesla", "Create a new password");
        public string Error_CouldNotSendEmail(string email) => GetTranslation($"Nepodařilo se odeslat email na adresu {email}", $"Couldnt send a mail to {email}");
        public string Error_BadAuthentication => GetTranslation("Vyskytl se problém s přihlášením.", "There was a problem with log in.");

        public string Error_CouldNotRefreshPassword => GetTranslation("Nepodařilo se obnovit heslo. Zkuste to znovu.",
            "Could not refresh the password. Try again.");

        internal string Error_UniversityNameOrUrlNotUniq(string name, string url) => GetTranslation($"Buď název univerzity '{name}' nebo url '{url}' je již zabrané.", $"The name of the university {name} or url {url} is not uniq.");

        public string Error_OldPasswordsDontMatch =>
            GetTranslation("Staré heslo není správné.", "The old password is not valid.");

        public string Error_WrongLoginCredentials =>
            GetTranslation("Zadány neplatné přihlašovací údaje.", "Wrong email or password.");

        public string Error_EmailHasBeenAlreadySent => GetTranslation(
            "Email byl již odeslán. Nezapomeňte zkontrolovat spam.",
            "Email has already been sent. Dont forget to check your spam.");

        public string Error_CouldNotEditPost => GetTranslation(
            "Nepodařilo se upravit příspěvek.", "Could not edit the desired post.");

        public string EmailVerifyRegistration_Title => GetTranslation("Potvrďte Váš email", "Confirm your email");

        public string EmailVerifyRegistration_Preheader => GetTranslation("Potvrďte Váš email kliknutím na odkaz", "Confirm your email by clicking on the given link");

        public string EmailVerifyRegistration_Header => GetTranslation("Potvrďte Váš email", "Confirm your email");

        public string EmailVerifyRegistration_Addressing => GetTranslation("Dobrý den,", "Hello,");

        public string EmailVerifyRegistration_Text => GetTranslation("Děkujeme, že jste se rozhodl/a stát se součástí Uniwiki. K dokončení již zbývá poslední krok, což je potvrzení emailu.", 
            "Thank you for becoming a member of Uniwiki. There is just one step left fo you, which is to confirm your email.");

        public string EmailVerifyRegistration_ButtonConfirmEmail => GetTranslation("Potvrdit email", "Confirm email");

        public string EmailVerifyRegistration_DisplayingProblems => GetTranslation("Pokud se Vám nezobrazuje email správně, můžete zkusit tento link",
            "If the email is not well displayed, you can try the following link.");

        public string EmailVerifyRegistration_ContactUs => GetTranslation("Neváhejte nás kontaktovat přes", "Do not hesitate to contact us through");

        public string EmailRestorePassword_Title => GetTranslation("Obnovit heslo", "Restore password");

        public string EmailRestorePassword_Preheader => GetTranslation("Obnovte vaše heslo kliknutím na odkaz", "Restore your password by clicking the link");

        public string EmailRestorePassword_Header => GetTranslation("Obnovit heslo", "Restore password");

        public string EmailRestorePassword_Text => GetTranslation("Každému se to čas od času stane :-)", "It happens to everyone from time to time :-)");

        public string EmailRestorePassword_ButtonRestorePassword => GetTranslation("Vytvořit nové heslo", "Create a new password");

        public string EmailRestorePassword_DisplayingProblems => GetTranslation("Pokud se Vám nezobrazuje email správně, můžete zkusit tento link", "If the email is not displayed properly, you can try clicking the following link ");

        public string EmailRestorePassword_ContactUs => GetTranslation("Případně nás neváhejte kontaktovat přes", "Do not hesitate to contact us via");

        public string ResendConfirmation_ProfileIsAlreadyConfirmed(string email) => GetTranslation(
            $"Email {email} byl již potvrzen.", 
            $"The email {email} was already confirmed.");

        public string Error_YourEmailWasNotYetConfirmed(string email) => GetTranslation(
            $"Váš účet nebyl aktivován. Aktivujte jej přes odkaz v emailu {email}. Případně opakujte registraci.",
            $"Your email was not confirmed yet. Confirm it through a link in your email {email}. Register again eventually.");

        public string Error_EmailIsAlreadyUsed(string email) => GetTranslation($"Email {email} je již používán.",
            $"The email {email} is already used.");

        public string Email_RestorePasswordMessage(string link) => GetTranslation(
            $"<a href=\"{link}\">Nastavit nové heslo</a>. Nebo klikněte na tento link {link}",
            $"<a href=\"{link}\">Create a new password</a>. Or click the following link {link}");

        public string Email_RegisterMessage(string link) => GetTranslation(
            $"<a href=\"{link}\">Potvrdit email</a><p>Případně klikněte na tento odkaz: {link}</p>",
            $"<a href=\"{link}\">Confirm the email</a><p> Or click the following link: {link}</p>");

        public string Error_CourseNameTaken(string name) => GetTranslation($"Předmět s názvem '{name}' již existuje.", $"Course '{name}' already exists.");

        public string Error_StudyGroupNameIsTaken(string name) => GetTranslation(
            $"Studijní skupina s názvem '{name}' již existuje.", $"Study group with the name '{name}' already exists.");
    }
}

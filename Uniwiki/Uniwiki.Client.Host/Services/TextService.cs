using Microsoft.AspNetCore.Components;
using System;
using System.Text.Encodings.Web;
using Uniwiki.Shared;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.Services;

namespace Uniwiki.Client.Host.Services
{
    // We dont want to abstract this one - translating would then be too much time consuming
    internal class TextService
    {
        private readonly ILanguageService _languageService;

        public TextService(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public string AddCoursePage_WhichFaculty 
            =>_languageService.GetTranslation("Na jakou univerzitu a fakultu má být předmět zařazen?", "At which university and faculty is the course taught?");
        public string RegisterPage_YouCanChangeItOnProfile =>_languageService.GetTranslation("(Můžete změnit později na vaší profilové stránce)", "(You can change it later on your profile page)");
        public string SearchBox_SearchingOnlyAtFaculty(StudyGroupDto faculty) =>_languageService.GetTranslation(
            $"(Hledáte pouze na { faculty.University.ShortName } { faculty.LongName })", 
            $"(Searching only at { faculty.University.ShortName } { faculty.LongName })");
        public string WhereDoYouStudy =>_languageService.GetTranslation("Kde studujete?", "Where do you study?");
        public string SelectUniversityAndFaculty =>_languageService.GetTranslation("Vybrat univerzitu a fakultu", "Select my university and faculty");
        public string OriginalFileName =>_languageService.GetTranslation("Původně ", "Originally ");
        public string RegisterPage_AgreeOn =>_languageService.GetTranslation("Souhlasím s ", "I agree with ");
        public string RegisterPage_TermsOfUse =>_languageService.GetTranslation("podmínkami použití", "the terms of use");
        public string ConfirmEmailPage_EmailSentAgain =>_languageService.GetTranslation("Potvrzovací email byl znovu odeslán", "The confirmation email has been sent again.");
        public string Message =>_languageService.GetTranslation("Text příspěvku", "Text");
        public string Files =>_languageService.GetTranslation("Soubory", "Files");
        public string ProvideFeedbackText =>_languageService.GetTranslation("Jak můžeme Uniwiki zlepšit? ", "How can we improve Uniwiki?");
        public string Toast_ThanksForFeedback =>_languageService.GetTranslation("Děkujeme za zpětnou vazbu!", "Thank you for your feedback!");
        public string Submit =>_languageService.GetTranslation("Odeslat", "Submit");
        public string Modal_ProvideFeedback =>_languageService.GetTranslation("Napište nám zpětnou vazbu", "Write a feedback for us");
        public string Modal_OverallRating =>_languageService.GetTranslation("Celkové hodnocení", "Overall rating");
        public string Modal_Feedback_Title =>_languageService.GetTranslation("Napište nám zpětnou vazbu", "Provide feedback");
        public string GoToUniwiki =>_languageService.GetTranslation("Přejít na Uniwiki", "Go to Uniwiki");
        public string AddCourse =>_languageService.GetTranslation("Přidat předmět", "Add course");
        public string SelectYourFaculty =>_languageService.GetTranslation("Vyberte vaši fakultu", "Select your faculty");
        public string DownloadErrorPage_Message =>_languageService.GetTranslation("Tento soubor se nám bohužel nepodařilo stáhnout :(.", "We somehow were not able to download this file :(.");
        public string DownloadTimeErrorPage_GoBack =>_languageService.GetTranslation("Návrat na Uniwiki", "Back to Uniwiki");
        public string DownloadTimeErrorPage_Message =>_languageService.GetTranslation("Pokusili jste se stáhnout soubor znovu příliš rychle.", "You tried to download the file again too quickly.");
        public string DownloadTimeErrorPage_Title =>_languageService.GetTranslation("Příliš brzy", "Too early");
        public string FileNotFoundPage_Title =>_languageService.GetTranslation("Soubor nenalezen!", "File not found!");
        public string NoFacultiesInUniversity =>_languageService.GetTranslation("Zatím zde nejsou žádné fakulty.", "There are no faculties yet.");
        public string ProfilePage_CannotUploadPictureYet =>_languageService.GetTranslation("Profilovou fotku zatím nahrát nelze.", "Its not possible to upload the profile picture yet.");

        public string Toast_ErrorWhileUploadingFile(string fileName) =>_languageService.GetTranslation(
            $"Vyskytl se problém během načítání souboru { _languageService.Sanitize(fileName) }",
            $"There was a problem while uploading the file {_languageService.Sanitize(fileName)}");

        public string ProfilePage_UploadProfilePicture =>_languageService.GetTranslation("Nahrát fotku", "Upload a picture");
        public string SearchBox_DefaultPlaceholderText =>_languageService.GetTranslation("Můžete začít zobrazením všech předmětů z vaší fakulty.", "You can start by showing all courses from your faculty.");
        public string SearchBox_ChooseFaculty =>_languageService.GetTranslation("Vybrat fakultu", "Select your faculty");
        public string HomePage_SearchCourse =>_languageService.GetTranslation("Hledat předmět", "Search course");
        public string SearchBox_AddCourse =>_languageService.GetTranslation("Přidat předmět", "Add course");
        public string SearchBox_NoCoursesInGroupYet =>_languageService.GetTranslation("Na této fakultě nejsou zatím žádné předměty", "There are no courses in this faculty yet.");
        public string Modal_ConfirmPostRemoval =>_languageService.GetTranslation("Opravdu chcete odstranit příspěvek?", "Are you sure to remove the post?");
        public string AddFacultyModal_Title =>_languageService.GetTranslation("Přidat fakultu", "Add a faculty");
        public string SearchBox_SelectGroup =>_languageService.GetTranslation("Předměty z fakulty", "Courses from a faculty");
        public string Language_Language =>_languageService.GetTranslation("Jazyk", "Language");
        public string Language_English =>_languageService.GetTranslation("Angličtina", "English");
        public string Edit =>_languageService.GetTranslation("Upravit", "Edit");
        public string Toast_YouNeedToLogIn =>_languageService.GetTranslation("Pro tuto akci se musíte přihlásit.", "You need to log in for this action.");
        public string NotFoundPage_Title =>_languageService.GetTranslation("Stránka nenalezena!", "Page not found!");
        public string NotFoundPage_BackToUniwiki =>_languageService.GetTranslation("Zpět na Uniwiki", "Back to Uniwiki");
        public string EditPost_EditPost =>_languageService.GetTranslation("Uložit změny", "Save changes");
        public string SearchBox_CourseCode =>_languageService.GetTranslation("Kód předmětu (nepovinný)", "Course code (optional)");
        public string SearchBox_CourseName =>_languageService.GetTranslation("Název předmětu", "Course name");
        public string SearchBox_FacultyShortcut =>_languageService.GetTranslation("Zkratka fakulty", "Shortcut of the faculty");
        public string SearchBox_FacultyName =>_languageService.GetTranslation("Název fakulty", "Faculty name");
        public string Toast_CourseCreatedTitle =>_languageService.GetTranslation("Předmět vytvořen", "Course created");
        public string Toast_FacultyCreatedTitle =>_languageService.GetTranslation("Fakulta vytvořena", "Faculty has been created");
        public string CoursePage_SearchType =>_languageService.GetTranslation("Jiná kategorie", "Other category");
        public string EmailConfirmedPage_TryAgain =>_languageService.GetTranslation("Zkusit znovu", "Try again");
        public string EmailConfirmedPage_FailMessage =>_languageService.GetTranslation("Nepodařilo se potvrdit email", "Could not confirm the email");
        public string EmailConfirmedPage_SuccessMessage =>_languageService.GetTranslation("Došlo k úspěšnému potvrzení emailu", "Sucessfully confirmed your email");
        public string EmailConfirmedPage_TitleConfirmingFail =>_languageService.GetTranslation("Email nebyl potvrzen", "Email not confirmed");
        public string EmailConfirmedPage_TitleConfirmingSuccess =>_languageService.GetTranslation("Email potvrzen", "Email confirmed");
        public string EmailConfirmedPage_TitleConfirming =>_languageService.GetTranslation("Potvrzování emailu", "Confirming the email");
        public string CoursePage_PostType =>_languageService.GetTranslation("Kategorie", "Category");
        public string SearchBox_RecentCourses =>_languageService.GetTranslation("Nedávné předměty", "Recent courses");
        public string Login =>_languageService.GetTranslation("Přihlásit se", "Log in");
        public string Error_ConnectionError =>_languageService.GetTranslation("Problém s připojením.", "Unable to connect.");
        public string Error_ErrorOnServer =>_languageService.GetTranslation("Na serveru se vyskytla chyba.", "There was an error on the server.");
        public string Error_InvalidSecretCode =>_languageService.GetTranslation("Zadaný kód je neplatný.", "The provided code is either used or broken.");

        public string HomePage_SubTitle =>_languageService.GetTranslation("Hledejte studijní materiály", "Search study materials");
        public string SearchCourse =>_languageService.GetTranslation("Předměty", "Courses");
        public string Button_SignOut =>_languageService.GetTranslation("Odhlásit se", "Sign out");
        public string Button_LogIn =>_languageService.GetTranslation("Přihlásit se", "Log in");
        public string Button_ChangePassword =>_languageService.GetTranslation("Změnit heslo", "Change password");
        public string Spinner_Loading =>_languageService.GetTranslation("Načítání", "Loading");

        public string PostType_Unknown_Singular =>_languageService.GetTranslation("Nezařazený", "Unknown");
        public string PostType_Unknown_Plural =>_languageService.GetTranslation("Nezařazené", "Unknown");
        public string PostType_All_Singular =>_languageService.GetTranslation("Vše", "All");
        public string CoursePage_AddPost =>_languageService.GetTranslation("Přidat příspěvek", "Add post");
        public string CoursePage_NumberOfFiles(int filesCount) =>_languageService.GetTranslation($"Soubory ({ filesCount })", $"Files ({ filesCount })");
        public string CoursePage_ShowAll =>_languageService.GetTranslation("Filtrovat kategorii", "Filter category");
        public string CoursePage_NoPostsPlaceholder =>_languageService.GetTranslation("Zde zatím nejsou žádné příspěvky.", "There are no posts yet.");
        public string ChangePasswordPage_Title =>_languageService.GetTranslation("Změna hesla", "Change your password");
        public string ChangePasswordPage_OldPassword =>_languageService.GetTranslation("Staré heslo", "Old password");
        public string ChangePasswordPage_NewPassword =>_languageService.GetTranslation("Nové heslo", "New password");
        public string ChangePasswordPage_NewPasswordAgain =>_languageService.GetTranslation("Nové heslo znovu", "The new password again");
        public string ChangePasswordPage_ChangePassword =>_languageService.GetTranslation("Změnit heslo", "Change your password");
        public string ConfirmEmailPage_Title =>_languageService.GetTranslation("Potvrďte email", "Confirm your email");
        public string ConfirmEmailPage_EmailSentMessage =>_languageService.GetTranslation(
            $"Odeslali jsme Vám potvrzovací email na ",
            $"We sent a confirmation email to ");
        public MarkupString ConfirmEmailPage_EmailNotReceivedMessage =>
            new MarkupString(_languageService.GetTranslation(
                "Často spadne zpráva do <strong>SPAMU</strong>, tak jej raději zkontrolujte.",
                "Check your <strong>SPAM</strong> firstly, if the email was not delivered."));
        public string ConfirmEmailPage_SelectAnotherEmail =>_languageService.GetTranslation("Zadat jiný email", "Use another email");
        public string ConfirmEmailPage_ReportProblem =>_languageService.GetTranslation("Nahlásit problém", "Report a problem");
        public string ConfirmEmailPage_SendEmailAgain =>_languageService.GetTranslation("Odeslat email znovu", "Send the email again");
        public string ConfirmEmailPage_InXSeconds(int seconds) =>_languageService.GetTranslation($"(za {seconds}s)", $"(in {seconds}s)");
        public string CreateNewPasswordPage_Title =>_languageService.GetTranslation("Změna hesla", "Password change");
        public string CreateNewPasswordPage_SetPassword =>_languageService.GetTranslation("Nastavit heslo", "Set the password");
        public string CreateNewPasswordPage_NewPassword =>_languageService.GetTranslation("Nové heslo", "New password");
        public string CreateNewPasswordPage_NewPasswordAgain =>_languageService.GetTranslation("Nové heslo znovu", "The new password again");
        public string EmailConfirmedPage_Title =>_languageService.GetTranslation("Potvrzení emailu", "Email confirmation");
        public string EmailConfirmedPage_ConfirmingEmail =>_languageService.GetTranslation("Potvrzování emailu", "Confirming the email");
        public string EmailConfirmedPage_EmailSuccessfullyConfirmed =>_languageService.GetTranslation("Váš email byl úspěšně potvrzen.", "Your email was successfully confirmed.");
        public string EmailConfirmedPage_NowYouCan =>_languageService.GetTranslation("Nyní se můžete", "Now, you can");
        public string EmailConfirmedPage_LogIn =>_languageService.GetTranslation("Přihlásit", "Log in");
        public string LoginPage_Title =>_languageService.GetTranslation("Přihlášení", "Log in");
        public string LoginPage_CreateNewAccount =>_languageService.GetTranslation("Vytvořit nový účet", "Create a new account");
        public string LoginPage_ForgottenPassword =>_languageService.GetTranslation("Zapomenuté heslo?", "Forgot password?");
        public string PasswordChangedPage_Title =>_languageService.GetTranslation("Heslo úspěšně nastaveno", "The password has been successfully set.");
        public string PasswordChangedPage_BackToUniwiki =>_languageService.GetTranslation("Návrat na Uniwiki", "Back to Uniwiki");
        public string RegisterPage_Title =>_languageService.GetTranslation("Nový účet", "New account");
        public string RegisterPage_NameAndSurname =>_languageService.GetTranslation("Jméno a příjmení", "Name and surname");
        public string RegisterPage_Surname =>_languageService.GetTranslation("Příjmení", "Surname");
        public string RegisterPage_Create =>_languageService.GetTranslation("Vytvořit účet", "Create a new account");
        public string RestorePasswordPage_Title =>_languageService.GetTranslation("Zapomenuté heslo", "Forgot password");
        public string RestorePasswordPage_YourEmail =>_languageService.GetTranslation("Váš email", "Your email");
        public string RestorePasswordPage_RestorePassword =>_languageService.GetTranslation("Obnovit heslo", "Restore password");
        public string RestorePasswordRequestedPage_Title =>_languageService.GetTranslation("Změna zapomenutého hesla", "Change forgotten password");
        public MarkupString RestorePasswordRequestedPage_EmailSentMessage(string email) => new MarkupString(_languageService.GetTranslation(
$"Na email <strong>{_languageService.Sanitize(email)}</strong> Vám byl odeslán odkaz pro vytvoření nového hesla. Pokud Vám mail nepřišel, tak nejprve zkontrolujte <strong>SPAM</strong>.",
$"The link to reset your password has been sent to your email <strong>{_languageService.Sanitize(email)}</strong>. In case the email was not delivered, check the <strong>SPAM</strong>."));

        public string SearchBox_Courses =>_languageService.GetTranslation("Předměty", "Courses");

        public string SearchBox_SearchCourse =>_languageService.GetTranslation("Hledat předmět...", "Search course...");
        public string ViaGoogle =>_languageService.GetTranslation("Přes Google", "Via Google");
        public string ViaFacebook =>_languageService.GetTranslation("Přes Facebook", "Via Facebook");
        public string Email =>_languageService.GetTranslation("Email", "Email");
        public string Password =>_languageService.GetTranslation("Heslo", "Password");
        public string PasswordAgain =>_languageService.GetTranslation("Heslo znovu", "Password again");
        public string LogIn =>_languageService.GetTranslation("Přihlásit se", "Log in");
        public string AddFiles =>_languageService.GetTranslation("Přidat soubory", "Add files");
        public string PassedTime_Years(int years) =>_languageService.GetTranslation($"Před {years} r", $"{years} y ago");
        public string PassedTime_Weeks(int weeks) =>_languageService.GetTranslation($"Před {weeks} t", $"{weeks} w ago");
        public string PassedTime_Days(int days) =>_languageService.GetTranslation($"Před {days} d", $"{days} d ago");
        public string PassedTime_Hours(int hours) =>_languageService.GetTranslation($"Před {hours} h", $"{hours} h ago");
        public string PassedTime_Minutes(int minutes) =>_languageService.GetTranslation($"Před {minutes} min", $"{minutes} min ago");
        public string PassedTime_Now =>_languageService.GetTranslation($"Právě teď", $"Just now");
        public string Toast_AppDidAutomaticLogout =>_languageService.GetTranslation($"Došlo k automatickému odhlášení.", $"You have been automatically logged out.");
        public string ToAddNewPost =>_languageService.GetTranslation("pro přidání nového příspěvku.", "to add a new post.");

        public string CoursePage_WriteYourMessageHere =>
           _languageService.GetTranslation("Napište zprávu k příspěvku", "Write your message here.");

        public string AddNewCourse =>_languageService.GetTranslation("Přidat předmět", "Add a new course");
        public string Create =>_languageService.GetTranslation("Vytvořit", "Create");

        public string Toast_FacultyCreated(string facultyLongName, string facultyShortName) =>_languageService.GetTranslation(
                $"Fakulta {facultyLongName} ({facultyShortName}) byla úspěšně vytvořena.",
                $"Faculty {facultyLongName} ({facultyShortName}) has been successfully created.");

        public string Toast_CourseCreated(string courseLongName, string courseShortName) =>_languageService.GetTranslation(
            $"Předmět { courseLongName } { courseShortName }byl úspěšně přidán",
            $"The course { courseLongName } { courseShortName }has been successfully created.");

        public string SearchBox_NoResults()
        {
            return _languageService.GetTranslation(
                $"Žádné předměty nenalezeny.",
                $"No courses found.");
        }

        public string FileSize(in long fileSizeInBytes)
        {
            if (fileSizeInBytes / 1_000 == 0)
                return "> 1 KB";

            if (fileSizeInBytes / 1_000_000 == 0)
                return (fileSizeInBytes / 1_000) + " KB";

            if (fileSizeInBytes / 1_000_000_000 == 0)
                return (fileSizeInBytes / 1_000_000) + " MB";

            return (fileSizeInBytes / 1_000_000_000) + " GB";
        }

        public string EditPost_FileAlreadySelected(string uploadFileName) 
            => _languageService.GetTranslation($"Soubor {uploadFileName} byl již vybrán.",
                $"The file {uploadFileName} is already selected.");

        public string EditPost_FileTooBig(string fileName, long fileSize) =>_languageService.GetTranslation(
            $"Soubor {fileName} je příliš velký. Maximální velikost je { FileSize(Constants.MaxFileSizeInBytes) }. Váš soubor má {FileSize(fileSize)}",
            $"The provided file {fileName} is too big. Maximal size is { FileSize(Constants.MaxFileSizeInBytes) }. Provided file has more than {FileSize(fileSize)}");

        public string EditPost_HasFailedFile =>_languageService.GetTranslation(
            "Některé soubory se nepovedlo nahrát. Zkuste je nahrát znovu nebo odstranit.",
            "Some of the files we were unable to upload. Reupload or remove them.");

        public string CoursePage_LoadMorePosts =>_languageService.GetTranslation("Načíst další příspěvky", "Load more posts");
        public string CoursePage_AllPostsShowed =>_languageService.GetTranslation("Zobrazeny všechny příspěvky.", "No more posts to show.");

        public string EditPost_MaxFileSize =>_languageService.GetTranslation($"Maximum {FileSize(Constants.MaxFileSizeInBytes)}",
            $"Maximum {FileSize(Constants.MaxFileSizeInBytes)}");

        public string Remove =>_languageService.GetTranslation("Odstranit", "Remove");
        public string Change =>_languageService.GetTranslation("Změnit", "Change");
        public string Language_Czech =>_languageService.GetTranslation("Čeština", "Czech");
        public string UniversitiesAndGroups =>_languageService.GetTranslation("Univerzity a fakulty", "Universities and faculties");
        public string Add =>_languageService.GetTranslation("Přidat", "Add");

        public string EgGroupName =>_languageService.GetTranslation("Např.: Fakulta Informačních Technologií",
            "E.g. Faculty of Information Technology");

        public string EgGroupShortcut =>_languageService.GetTranslation("Např.: FIT", "E.g. FIT");
        public string University =>_languageService.GetTranslation("Univerzita", "University");
        public string Faculty =>_languageService.GetTranslation("Fakulta", "Faculty");
        public string Course =>_languageService.GetTranslation("Předmět", "Course");
        public string SelectFaculty =>_languageService.GetTranslation("Vybrat fakultu", "Select faculty");
        public string EgCourseName =>_languageService.GetTranslation("Např.: Ekonomie", "E. g. Economy");
        public string EgCourseCode =>_languageService.GetTranslation("Např.: Eko", "E. g. Eco");
        public string Yes =>_languageService.GetTranslation("Ano", "Yes");
        public string No =>_languageService.GetTranslation("Ne", "No");
        public string CreateGroup =>_languageService.GetTranslation("Přidat fakultu", "Add a faculty");

        public string MissingUniversityOrFacultyQuestion =>_languageService.GetTranslation("Chybí zde vaše univerzita nebo fakulta? napište nám na ",
            "Dont see your university or faculty? Write us on ");

        public string Error_UnableToResolveSecret =>
           _languageService.GetTranslation("Tento odkaz je neplatný.", "This link is not valid.");

        public string DownloadErrorPage_Title =>_languageService.GetTranslation("Error", "Error");

        public string FileNotFoundPage_LostFile =>
           _languageService.GetTranslation("Tento soubor jsme jaksi ztratili.", "We kind of lost this file.");

        public string FileNotFoundPage_GoBack =>_languageService.GetTranslation("Návrat na Uniwiki", "Go back to Uniwiki");
        public string AddComent =>_languageService.GetTranslation("Napište komentář", "Write a comment");

        public string EditPost_UploadingFiles(in int filesCount) =>
           _languageService.GetTranslation($"Nahrávání souborů ({filesCount})", $"Uploading of {filesCount} files");

        public string PostFile_TryDownloadAgainInXSeconds(in int periodsLeft) =>
           _languageService.GetTranslation($"(Znovu za {periodsLeft} s)", $"(Try again in {periodsLeft} s)");

        public string Modal_ConfirmPostFileRemoval(string fileName) =>_languageService.GetTranslation(
            $"Opravdu chcete odstranit soubor '{_languageService.Sanitize(fileName)}'",
            $"Are you sure to remove the file '{_languageService.Sanitize(fileName)}'");

        public string SelectFacultyModal_Title =>
           _languageService.GetTranslation("Vyberte vaši fakultu", "Select your faculty");

        public string ErrorPage_Title =>
           _languageService.GetTranslation("Error", "Error");
        public string ErrorPage_Message =>
           _languageService.GetTranslation("Na serveru se vyskytl problém :(.", "We encountered an error on the server :(.");

        public string ErrorPage_GoBack =>
           _languageService.GetTranslation("Zpět na Uniwiki", "Back to Uniwiki");

        public string Toast_Info =>_languageService.GetTranslation("Info", "Info");

        public string Toast_Error =>_languageService.GetTranslation("Error", "Error");
        public string Toast_Warning =>_languageService.GetTranslation("Upozornění", "Warning");
        public string Toast_Success =>_languageService.GetTranslation("Úspěch", "Success");

        public string ProfilePage_ConfirmRemoveHomeUniversityAndFaculty =>_languageService.GetTranslation("Opravdu chcete odebrat vaši univerzitu a fakultu?", "Are you sure to remove your university and faculty");
    }
}

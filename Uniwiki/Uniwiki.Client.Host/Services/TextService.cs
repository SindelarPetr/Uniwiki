using Uniwiki.Shared;
using Uniwiki.Shared.Services;

namespace Uniwiki.Client.Host.Services
{
    // We dont want to abstract this one - translating would then be too much time consuming
    internal class TextService : TextServiceBase
    {
        public string GoToUniwiki => GetTranslation("Přejít na Uniwiki", "Go to Uniwiki");
        public string AddCourse => GetTranslation("Přidat předmět", "Add course");
        public string SelectYourFaculty => GetTranslation("Vyberte vaši fakultu", "Select your faculty");
        public string DownloadErrorPage_Message => GetTranslation("Tento soubor se nám bohužel nepodařilo stáhnout :(.", "We somehow were not able to download this file :(.");
        public string DownloadTimeErrorPage_GoBack => GetTranslation("Návrat na Uniwiki", "Back to Uniwiki"); // TODO: Finish
        public string DownloadTimeErrorPage_Message => GetTranslation("Pokusili jste se stáhnout soubor znovu příliš rychle.", "You tried to download the file again too quickly.");
        public string DownloadTimeErrorPage_Title => GetTranslation("Příliš brzy", "Too early");
        public string FileNotFoundPage_Title => GetTranslation("Soubor nenalezen!", "File not found!");
        public string NoStudyGroupsInUniversity =>GetTranslation("Zatím zde nejsou žádné fakulty.", "There are no faculties yet.");
        public string ProfilePage_CannotUploadPictureYet => GetTranslation("Profilovou fotku zatím nahrát nelze.", "Its not possible to upload the profile picture yet.");
        public string ProfilePage_UploadProfilePicture => GetTranslation("Nahrát fotku", "Upload a picture");
        public string SearchBox_DefaultPlaceholderText => GetTranslation("Můžete začít zobrazením všech předmětů z vaší fakulty.", "You can start by showing all courses from your faculty.");
        public string SearchBox_ChooseStudyGroup => GetTranslation("Vybrat fakultu", "Select your faculty");
        public string HomePage_SearchCourse => GetTranslation("Hledat předmět", "Search course");
        public string SearchBox_AddCourse => GetTranslation("Přidat předmět", "Add course");
        public string SearchBox_NoCoursesInGroup => GetTranslation("Na této fakultě nejsou zatím žádné předměty", "There are no courses in this faculty yet.");
        public string Modal_ConfirmPostRemoval => GetTranslation("Opravdu chcete odstranit příspěvek?", "Are you sure to remove the post?");
        public string AddStudyGroupModal_Title => GetTranslation("Přidat fakultu", "Add a faculty");
        public string SearchBox_SelectGroup => GetTranslation("Vybrat fakultu", "Select a faculty");
        public string Language_Language => GetTranslation("Jazyk", "Language");
        public string Language_English => GetTranslation("Angličtina", "English");
        public string Edit => GetTranslation("Upravit", "Edit");
        public string Toast_YouNeedToLogIn => GetTranslation("Pro tuto akci se musíte přihlásit.", "You need to log in for this action.");
        public string NotFoundPage_Title => GetTranslation("Stránka nenalezena!", "Page not found!");
        public string NotFoundPage_BackToUniwiki => GetTranslation("Zpět na Uniwiki", "Back to Uniwiki");
        public string EditPost_EditPost => GetTranslation("Uložit změny", "Save changes");
        public string SearchBox_CourseCode => GetTranslation("Kód předmětu (nepovinný)", "Course code (optional)");
        public string SearchBox_CourseName => GetTranslation("Název předmětu", "Course name");
        public string SearchBox_StudyGroupShortcut => GetTranslation("Zkratka fakulty", "Shortcut of the faculty");
        public string SearchBox_StudyGroupName => GetTranslation("Název fakulty", "Faculty name");
        public string Toast_CourseCreatedTitle => GetTranslation("Předmět vytvořen", "Course created");
        public string Toast_StudyGroupCreatedTitle => GetTranslation("Fakulta vytvořena", "Faculty has been created");
        public string CoursePage_SearchType => GetTranslation("Jiná kategorie", "Other category");
        public string EmailConfirmedPage_TryAgain => GetTranslation("Zkusit znovu", "Try again");
        public string EmailConfirmedPage_FailMessage => GetTranslation("Nepodařilo se potvrdit email", "Could not confirm the email");
        public string EmailConfirmedPage_SuccessMessage => GetTranslation("Došlo k úspěšnému potvrzení emailu", "Sucessfully confirmed your email");
        public string EmailConfirmedPage_TitleConfirmingFail => GetTranslation("Email nebyl potvrzen", "Email not confirmed");
        public string EmailConfirmedPage_TitleConfirmingSuccess => GetTranslation("Email potvrzen", "Email confirmed");
        public string EmailConfirmedPage_TitleConfirming => GetTranslation("Potvrzování emailu", "Confirming the email");
        public string CoursePage_PostType => GetTranslation("Kategorie", "Category");
        public string SearchBox_RecentCourses => GetTranslation("Nedávné předměty", "Recent courses");
        public string Login => GetTranslation("Přihlásit se", "Log in");
        public string Error_ConnectionError => GetTranslation("Problém s připojením.", "Unable to connect.");
        public string Error_ErrorOnServer => GetTranslation("Na serveru se vyskytla chyba.", "There was an error on the server.");
        public string Error_InvalidSecretCode => GetTranslation("Zadaný kód je neplatný.", "The provided code is either used or broken.");

        public string HomePage_SubTitle => GetTranslation("Hledejte studijní materiály", "Search study materials");
        public string SearchCourse => GetTranslation("Předměty", "Courses");
        public string Button_SignOut => GetTranslation("Odhlásit se", "Sign out");
        public string Button_LogIn => GetTranslation("Přihlásit se", "Log in");
        public string Button_ChangePassword => GetTranslation("Změnit heslo", "Change password");
        public string Spinner_Loading => GetTranslation("Načítání", "Loading");

        public string PostType_Unknown_Singular => GetTranslation("Nezařazený", "Unknown");
        public string PostType_Unknown_Plural => GetTranslation("Nezařazené", "Unknown");
        public string PostType_All_Singular => GetTranslation("Vše", "All");
        public string CoursePage_AddPost => GetTranslation("Přidat příspěvek", "Add post");
        public string CoursePage_NumberOfFiles(int filesCount) => GetTranslation($"Soubory ({ filesCount })", $"Files ({ filesCount })");
        public string CoursePage_ShowAll => GetTranslation("Filtrovat kategorii", "Filter category");
        public string CoursePage_NoPostsPlaceholder => GetTranslation("Zde zatím nejsou žádné příspěvky.", "There are no posts yet.");
        public string ChangePasswordPage_Title => GetTranslation("Změna hesla", "Change your password");
        public string ChangePasswordPage_OldPassword => GetTranslation("Staré heslo", "Old password");
        public string ChangePasswordPage_NewPassword => GetTranslation("Nové heslo", "New password");
        public string ChangePasswordPage_NewPasswordAgain => GetTranslation("Nové heslo znovu", "The new password again");
        public string ChangePasswordPage_ChangePassword => GetTranslation("Změnit heslo", "Change your password");
        public string ConfirmEmailPage_Title => GetTranslation("Potvrďte email", "Confirm your email");
        public string ConfirmEmailPage_EmailSentMessage => GetTranslation($"Potvrďte Váš email kliknutím na odkaz odeslaný na ",
            $"Confirm your email by clicking on the link sent to ");
        public string ConfirmEmailPage_EmailNotReceivedMessage => GetTranslation("Pokud vám nepřišel, tak si nejprve zkontrolujte spam.", "Check your spam firstly, if the email was not delivered.");
        public string ConfirmEmailPage_SelectAnotherEmail => GetTranslation("Zadat jiný email", "Use another email");
        public string ConfirmEmailPage_ReportProblem => GetTranslation("Nahlásit problém", "Report a problem");
        public string ConfirmEmailPage_SendEmailAgain => GetTranslation("Odeslat email znovu", "Send the email again");
        public string ConfirmEmailPage_InXSeconds(int seconds) => GetTranslation($"(za {seconds}s)", $"(in {seconds}s)");
        public string CreateNewPasswordPage_Title => GetTranslation("Změna hesla", "Password change");
        public string CreateNewPasswordPage_SetPassword => GetTranslation("Nastavit heslo", "Set the password");
        public string CreateNewPasswordPage_NewPassword => GetTranslation("Nové heslo", "New password");
        public string CreateNewPasswordPage_NewPasswordAgain => GetTranslation("Nové heslo znovu", "The new password again");
        public string EmailConfirmedPage_Title => GetTranslation("Potvrzení emailu", "Email confirmation");
        public string EmailConfirmedPage_ConfirmingEmail => GetTranslation("Potvrzování emailu", "Confirming the email");
        public string EmailConfirmedPage_EmailSuccessfullyConfirmed => GetTranslation("Váš email byl úspěšně potvrzen.", "Your email was successfully confirmed.");
        public string EmailConfirmedPage_NowYouCan => GetTranslation("Nyní se můžete", "Now, you can");
        public string EmailConfirmedPage_LogIn => GetTranslation("Přihlásit", "Log in");
        public string LoginPage_Title => GetTranslation("Přihlášení", "Log in");
        public string LoginPage_CreateNewAccount => GetTranslation("Vytvořit nový účet", "Create a new account");
        public string LoginPage_ForgottenPassword => GetTranslation("Zapomenuté heslo?", "Forgot password?");
        public string PasswordChangedPage_Title => GetTranslation("Heslo úspěšně nastaveno", "The password has been successfully set.");
        public string PasswordChangedPage_BackToUniwiki => GetTranslation("Návrat na Uniwiki", "Back to Uniwiki");
        public string RegisterPage_Title => GetTranslation("Nový účet", "New account");
        public string RegisterPage_Name => GetTranslation("Jméno", "Name");
        public string RegisterPage_Surname => GetTranslation("Příjmení", "Surname");
        public string RegisterPage_Create => GetTranslation("Vytvořit účet", "Create a new account");
        public string RestorePasswordPage_Title => GetTranslation("Zapomenuté heslo", "Forgot password");
        public string RestorePasswordPage_YourEmail => GetTranslation("Váš email", "Your email");
        public string RestorePasswordPage_RestorePassword => GetTranslation("Obnovit heslo", "Restore password");
        public string RestorePasswordRequestedPage_Title => GetTranslation("Změna zapomenutého hesla", "Change forgotten password");
        public string RestorePasswordRequestedPage_EmailSentMessage(string email) => GetTranslation(
            $"Na email {email} Vám byl odeslán odkaz pro vytvoření nového hesla. Pokud Vám mail nepřišel, tak nejprve zkontrolujte spam.",
            $"The link to reset your password has been sent to your email {email}. In case the email was not delivered, check the email.");
        public string SearchBox_Courses => GetTranslation("Předměty", "Courses");
        
        public string SearchBox_SearchCourse => GetTranslation("Hledat předmět...", "Search course...");
        public string ViaGoogle => GetTranslation("Přes Google", "Via Google");
        public string ViaFacebook => GetTranslation("Přes Facebook", "Via Facebook");
        public string Email => GetTranslation("Email", "Email");
        public string Password => GetTranslation("Heslo", "Password");
        public string PasswordAgain => GetTranslation("Heslo znovu", "Password again");
        public string LogIn => GetTranslation("Přihlásit se", "Log in");
        public string AddFiles => GetTranslation("Přidat soubory", "Add files");
        public string PassedTime_Years(int years) => GetTranslation($"{years} r", $"{years} y");
        public string PassedTime_Weeks(int weeks) => GetTranslation($"{weeks} t", $"{weeks} w");
        public string PassedTime_Days(int days) => GetTranslation($"{days} d", $"{days} d");
        public string PassedTime_Hours(int hours) => GetTranslation($"{hours} h", $"{hours} h");
        public string PassedTime_Minutes(int minutes) => GetTranslation($"{minutes} min", $"{minutes} min");
        public string PassedTime_Now => GetTranslation($"Právě teď", $"Just now");
        public string Toast_AppDidAutomaticLogout => GetTranslation($"Došlo k automatickému odhlášení.", $"You have been automatically logged out.");
        public string ToAddNewPost => GetTranslation("pro přidání nového příspěvku.", "to add a new post.");

        public string CoursePage_WriteYourMessageHere =>
            GetTranslation("Napište zprávu k příspěvku", "Write your message here.");

        public string AddNewCourse => GetTranslation("Přidat předmět", "Add a new course");
        public string Create => GetTranslation("Vytvořit", "Create");

        public string Toast_StudyGroupCreated(string studyGroupLongName, string studyGroupShortName)
        {
            return GetTranslation(
                $"Fakulta {studyGroupLongName} ({studyGroupShortName}) byla úspěšně vytvořena.",
                $"Faculty {studyGroupLongName} ({studyGroupShortName}) has been successfully created.");
        }

        public string Toast_CourseCreated(string courseLongName, string courseShortName)
        {
            courseShortName = string.IsNullOrWhiteSpace(courseShortName) ? string.Empty : ("(" + courseShortName + ") ");
            return GetTranslation($"Předmět {courseLongName} {courseShortName}byl úspěšně přidán",
                $"The course {courseLongName} {courseShortName}has been successfully created.");
        }

        public string SearchBox_NoResults()
        {
            return GetTranslation(
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
        {
            return GetTranslation($"Soubor {uploadFileName} byl již vybrán.",
                $"The file {uploadFileName} is already selected.");
        }

        public string EditPost_FileTooBig(string fileName, long fileSize)
        {
            return GetTranslation($"Soubor {fileName} je příliš velký. Maximální velikost je { FileSize(Constants.MaxFileSizeInBytes) }. Váš soubor má {FileSize(fileSize)}", $"The provided file {fileName} is too big. Maximal size is { FileSize(Constants.MaxFileSizeInBytes) }. Provided file has more than {FileSize(fileSize)}");
        }

        public string EditPost_HasFailedFile => GetTranslation(
            "Některé soubory se nepovedlo nahrát. Zkuste je nahrát znovu nebo odstranit.",
            "Some of the files we were unable to upload. Reupload or remove them.");

        public string CoursePage_LoadMorePosts => GetTranslation("Načíst další příspěvky", "Load more posts");
        public string CoursePage_AllPostsShowed => GetTranslation("Zobrazeny všechny příspěvky.", "No more posts to show.");

        public string EditPost_MaxFileSize => GetTranslation($"Maximum {FileSize(Constants.MaxFileSizeInBytes)}",
            $"Maximum {FileSize(Constants.MaxFileSizeInBytes)}");

        public string Remove => GetTranslation("Odstranit", "Remove");
        public string Language_Czech => GetTranslation("Čeština", "Czech");
        public string UniversitiesAndGroups => GetTranslation("Univerzity a fakulty", "Universities and faculties");
        public string Add => GetTranslation("Přidat", "Add");

        public string EgGroupName => GetTranslation("Např.: Fakulta Informačních Technologií",
            "E.g. Faculty of Information Technology");

        public string EgGroupShortcut => GetTranslation("Např.: FIT", "E.g. FIT");
        public string University => GetTranslation("Univerzita", "University");
        public string StudyGroup => GetTranslation("Fakulta", "Faculty");
        public string Course => GetTranslation("Předmět", "Course");
        public string SelectStudyGroup => GetTranslation("Vybrat fakultu", "Select faculty");
        public string EgCourseName => GetTranslation("Např.: Ekonomie", "E. g. Economy");
        public string EgCourseCode => GetTranslation("Např.: Eko", "E. g. Eco");
        public string Yes => GetTranslation("Ano", "Yes");
        public string No => GetTranslation("Ne", "No");
        public string CreateGroup => GetTranslation("Přidat fakultu", "Add a faculty");

        public string MissingUniversityOrFacultyQuestion => GetTranslation("Chybí zde vaše univerzita nebo fakulta? napište nám na ",
            "Dont see your university or faculty? Write us on ");

        public string Error_UnableToResolveSecret =>
            GetTranslation("Tento odkaz je neplatný.", "This link is not valid.");

        public string DownloadErrorPage_Title => GetTranslation("Error", "Error");

        public string FileNotFoundPage_LostFile =>
            GetTranslation("Tento soubor jsme jaksi ztratili.", "We kind of lost this file.");

        public string FileNotFoundPage_GoBack => GetTranslation("Návrat na Uniwiki", "Go back to Uniwiki");
        public string AddComent => GetTranslation("Napište komentář", "Write a comment");

        public string EditPost_UploadingFiles(in int filesCount) =>
            GetTranslation($"Nahrávání souborů ({filesCount})", $"Uploading of {filesCount} files");

        public string PostFile_TryDownloadAgainInXSeconds(in int periodsLeft) =>
            GetTranslation($"(Znovu za {periodsLeft} s)", $"(Try again in {periodsLeft} s)");

        public string Modal_ConfirmPostFileRemoval(string fileName) => GetTranslation($"Opravdu chcete odstranit soubor '{fileName}'", $"Are you sure to remove the file '{fileName}'");

        public string SelectStudyGroupModal_Title => GetTranslation("Vybrat univerzitu" , "Select a university");
    }
}

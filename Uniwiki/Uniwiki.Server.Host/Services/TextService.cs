using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uniwiki.Shared;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Server.Host.Services
{
    public class TextService
    {
        private readonly ILanguageService _languageService;

        public TextService(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        internal string Error_IdentityValidationFailed =>
            _languageService.GetTranslation("Selhalo ověření identity.", "Failed to verify your identity.");

        public string Error_RefreshBrowser =>
            _languageService.GetTranslation("Aktualizujte verzi aplikace obnovením stránky v prohlížeči.", "Please refresh the page to use the new version of the app.");

        public string Error_OldVersionOfAppUsed => _languageService.GetTranslation(
            "Používáte starou verzi aplikace. Obnovíme pro vás stránku  pro získání nejnovější verze.",
            "There is a newer version of the app you are using. We will get a new one by refreshing the browser.");

        public string Error_ServerError => _languageService.GetTranslation("Na serveru se vyskytla chyba", "There was an error on the server.");

        public string Error_NoFilesReceived => _languageService.GetTranslation("Neobdrželi jsme žádné soubory :(.", "Didnt receive any files: (");

        public string Error_TooManyFiles => _languageService.GetTranslation("Obdrželi jsme příliš souborů. Je třeba poslat jeden soubor po druhém.", "Received too many files. Send just one file at a time.");

        public string Error_CouldNotDeserializeRequest => _languageService.GetTranslation("Nepovedlo se nám deserializovat obdržený požadavek.", "We were not able to deserialize the received request.");

        public string Error_DataFieldIsMissing =>
            _languageService.GetTranslation($"Požadavek neobsahuje  '{Constants.FileUploadDataField}'", $"The request does not contain {Constants.FileUploadDataField}");
    }
}

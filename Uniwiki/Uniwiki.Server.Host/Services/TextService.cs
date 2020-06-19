using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uniwiki.Shared.Services;

namespace Uniwiki.Server.Host.Services
{
    internal class TextService
    {
        private readonly TextServiceBase _textServiceBase;

        public TextService(TextServiceBase textServiceBase)
        {
            _textServiceBase = textServiceBase;
        }

        internal string Error_IdentityValidationFailed =>
            _textServiceBase.GetTranslation("Selhalo ověření identity.", "Failed to verify your identity.");

        public string Error_RefreshBrowser =>
            _textServiceBase.GetTranslation("Aktualizujte verzi aplikace obnovením stránky v prohlížeči.", "Please refresh the page to use the new version of the app.");

        public string Error_OldVersionOfAppUsed => _textServiceBase.GetTranslation(
            "Používáte starou verzi aplikace. Obnovíme pro vás stránku  pro získání nejnovější verze.",
            "There is a newer version of the app you are using. We will get a new one by refreshing the browser.");

        public string Error_ServerError => _textServiceBase.GetTranslation("Na serveru se vyskytla chyba", "There was an error on the server.");
    }
}

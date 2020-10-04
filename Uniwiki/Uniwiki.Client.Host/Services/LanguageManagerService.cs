using System;
using System.Threading.Tasks;
using Shared;
using Uniwiki.Client.Host.Services.Abstractions;
using Uniwiki.Shared;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Client.Host.Services
{
    internal class LanguageManagerService : ILanguageManagerService
    {
        private readonly ILanguageService _languageService;
        private readonly IJsInteropService _jsInteropService;
        private readonly ILocalStorageManagerService _localStorageManagerService;
        public Language CurrentLanguage => _languageService.Language;

        public LanguageManagerService(ILanguageService languageService, IJsInteropService jsInteropService,
            ILocalStorageManagerService localStorageManagerService)
        {
            _languageService = languageService;
            _jsInteropService = jsInteropService;
            _localStorageManagerService = localStorageManagerService;
        }

        public async Task InitializeLanguage()
        {
            // Try to get current language from local storage
            var language = await _localStorageManagerService.GetCurrentLanguage();

            // If the language is not present in local storage, then get it from the browser
            if (language == null)
            {
                var langCode = await _jsInteropService.GetBrowserLanguage();
                language = GetLanguageFromCode(langCode);
            }

            // Set it in the app
            await SetLanguage(language.Value);
        }

        public Task SetLanguage(Language language)
        {
            // Set language to texts
            _languageService.SetLanguage(language);

            // Set the language to the storage
            return _localStorageManagerService.SetCurrentLanguage(language);
        }

        private Language GetLanguageFromCode(string code)
        {
            // English
            if (code.ToLower().StartsWith("en"))
            {
                return Language.English;
            }

            // Czech
            if (code.ToLower().StartsWith("cs"))
            {
                return Language.Czech;
            }

            // Default
            return Constants.DefaultLanguage;
        }

        private string GetCodeFromLanguage(Language language) => language switch
        {
            Language.Czech => "cs",
            Language.English => "en",
            _ => throw new ArgumentException("Cannot find the language: " + language)
        };
    }
}
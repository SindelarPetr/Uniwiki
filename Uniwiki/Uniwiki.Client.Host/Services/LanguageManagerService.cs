using System;
using System.Threading.Tasks;
using Shared;
using Uniwiki.Client.Host.Services.Abstractions;
using Uniwiki.Shared;

namespace Uniwiki.Client.Host.Services
{
    internal class LanguageManagerService : ILanguageManagerService
    {
        private readonly TextService _textService;
        private readonly IJsInteropService _jsInteropService;
        private readonly ILocalStorageManagerService _localStorageManagerService;
        public Language CurrentLanguage => _textService.Language;

        public LanguageManagerService(TextService textService, IJsInteropService jsInteropService,
            ILocalStorageManagerService localStorageManagerService)
        {
            _textService = textService;
            _jsInteropService = jsInteropService;
            _localStorageManagerService = localStorageManagerService;
        }

        public async Task InitializeLanguage()
        {
            Console.WriteLine("Initializing language");

            // Try to get current language from local storage
            var language = await _localStorageManagerService.GetCurrentLanguage();

            Console.WriteLine("From local storage lang: " + language);

            // If the language is not present in local storage, then get it from the browser
            if (language == null)
            {
                var langCode = await _jsInteropService.GetBrowserLanguage();
                language = GetLanguageFromCode(langCode);
                Console.WriteLine("From browser lang: " + langCode);
            }

            // Get language from code
            
            Console.WriteLine("Got language from code: " + language);

            // Set it in the app
            await SetLanguage(language.Value);
        }

        public Task SetLanguage(Language language)
        {
            // Set language to texts
            _textService.SetLanguage(language);

            // Set the language to the storage
            return _localStorageManagerService.SetCurrentLanguage(language);
        }

        private Language GetLanguageFromCode(string code)
        {
            // English
            if (code.ToLower().StartsWith("en"))
                return Language.English;

            // Czech
            if (code.ToLower().StartsWith("cs"))
                return Language.Czech;

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
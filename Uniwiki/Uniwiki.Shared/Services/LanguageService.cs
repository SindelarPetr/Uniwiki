using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Shared.Services
{
    internal class LanguageService : ILanguageService
    {
        public Language Language { get; private set; }

        public string GetTranslation(string czech, string english) => GetTranslation<string>(czech, english);

        public void SetLanguage(Language language) => Language = language;

        public string Sanitize(string text) => HtmlEncoder.Default.Encode(text);

        public T GetTranslation<T>(T czech, T english) => Language == Language.Czech ? czech : english;
    }
}

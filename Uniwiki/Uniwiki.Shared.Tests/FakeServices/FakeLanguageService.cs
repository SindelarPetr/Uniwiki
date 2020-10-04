using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Shared.Tests.FakeServices
{
    public class FakeLanguageService : ILanguageService
    {
        public Language Language { get; private set; } = Constants.DefaultLanguage;

        public string GetTranslation(string czech, string english)
            => Language.Czech == Language ? czech : english;

        public T GetTranslation<T>(T czech, T english) => Language.Czech == Language ? czech : english;

        public string Sanitize(string text) => HttpUtility.HtmlEncode(text);

        public void SetLanguage(Language language) => Language = language;
    }
}

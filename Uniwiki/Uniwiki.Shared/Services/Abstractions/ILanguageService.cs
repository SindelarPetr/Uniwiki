using Shared;

namespace Uniwiki.Shared.Services.Abstractions
{
    public interface ILanguageService
    {
        public Language Language { get; }

        public void SetLanguage(Language language);

        public string GetTranslation(string czech, string english);
        public T GetTranslation<T>(T czech, T english);

        string Sanitize(string text);
    }
}

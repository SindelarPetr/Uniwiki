namespace Shared
{
    public enum Language { English = 1, Czech = 2}

    public static class Languages
    {
        public static Language DefaultLanguage { get; } = Language.English;

        public static Language Parse(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return DefaultLanguage;
            }

            var cleanCode = code.Split('-')[0];

            switch (cleanCode)
            {
                case "cs":
                    return Language.Czech;
                case "en":
                    return Language.English;
                default:
                    return DefaultLanguage;
            }
        }

        public static string LanguageToString(Language language)
        {
            return language.ToString();
        }
    }
}

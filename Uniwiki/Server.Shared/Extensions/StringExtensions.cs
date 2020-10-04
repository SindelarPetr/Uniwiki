using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Shared.Extensions
{
    public static class StringExtensions
    {
        public static string Neutralize(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            text = text.Normalize(NormalizationForm.FormD).ToLower();
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        public static string FormatString(this string text, params object[] args)
        {
            if (text == null)
            {
                return null;
            }

            var argsWithoutNulls = args.Select(a => a ?? string.Empty).ToArray();
            return string.Format(text, argsWithoutNulls);
        }

        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => string.Empty,
                _ => input.First().ToString().ToUpper() + input.Substring(1)
            };
    }
}

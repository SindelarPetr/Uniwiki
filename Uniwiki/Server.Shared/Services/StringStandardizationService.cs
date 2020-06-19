using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Shared.Extensions;

namespace Shared.Services
{
    internal class StringStandardizationService : IStringStandardizationService
    {
        public string CreateUrl(string text, Func<string, bool> isUniq)
        {
            string url;
            int? salt = null;
            do
            {
               url = CreateUrl(text, salt);
                salt = salt == null ? 1 : salt + 1;
            } while (!isUniq(url));

            return url;
        }

        private string CreateUrl(string text, int? salt)
        {
            return HttpUtility.UrlEncode(Regex.Replace(text.ToLower(), @"\s+", string.Empty)).Where(char.IsLetter)
                       .Aggregate(string.Empty, (s, c) => s + c) + (salt == null ? "" : salt.ToString());
        }

        /// <summary>
        /// Replaces all multiple whitespace characters by a single space character and trims whitespaces from both sides. 
        /// </summary>
        public string OptimizeWhiteSpaces(string? text) => text != null ? Regex.Replace(text, @"\s+", " ").Trim() : null;

        public string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        public string StandardizeSearchText(string text)
        {
            var reducedWhiteSpaceText = Regex.Replace(text, @"\s+", " ").ToLower().Trim();
            return RemoveAccents(reducedWhiteSpaceText);
        }

        public string StandardizeEmail(string email)
        {
            return email.Trim().ToLower();
        }

        public string StandardizeName(string name)
        {
            return OptimizeWhiteSpaces(name).FirstCharToUpper();
        }
    }
}

using System;
using System.Collections.Specialized;
using System.Web;

namespace Uniwiki.Shared
{
    public static class RouteHelper
    {
        /// <summary>
        /// Concatenate all the given strings with the character '/' between them. The character is not placed at the end or start
        /// </summary>
        /// <param name="parts">The strings to concatenate</param>
        /// <returns>The concatenated strings by '/' character. Null returns null. Empty parts return empty string.</returns>
        internal static string BuildRouteParts(params string[] parts)
        {
            if (parts == null)
                return string.Empty;

            if (parts.Length == 0)
                return string.Empty;

            string result = parts[0];
            for (var i = 1; i < parts.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(parts[i]))
                {
                    // Encode the value to URL-friendly format
                    var urlFriendlyPart = HttpUtility.UrlEncode(parts[i]);

                    result = result + "/" + urlFriendlyPart;
                }
            }

            return result;
        }

        internal static string BuildRoutePartsWithParameters(NameValueCollection parameters, params string[] parts)
        {
            var parameterString = string.Empty;
            for (var i = 0; i < parameters.AllKeys.Length; i++)
            {
                // Add '&' character between 2 parameters
                if (i != 0)
                {
                    parameterString += "&";
                }

                // Add the new parameter to the parameterString
                var key = parameters.AllKeys[i];
                var value = HttpUtility.UrlEncode(parameters[key]);
                parameterString += $"{key}={value}";
            }

            return BuildRouteParts(parts) + (string.IsNullOrWhiteSpace(parameterString) ? string.Empty : "?" + parameterString);
        }

        internal static string? TryGetQueryParameterValue(string url, string parameterKey)
        {
            Uri myUri = new Uri(url);
            return HttpUtility.ParseQueryString(myUri.Query).Get(parameterKey);
        }
    }
}

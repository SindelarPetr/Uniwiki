using System;
using System.Collections.Specialized;
using System.Web;
using Shared;

namespace Uniwiki.Shared
{
    public static class ApiRoutes
    {
        public static class UploadController
        {
            public const string FileIdParameter = "FileId";
            public const string SecondaryTokenParameter = "Token";
            public const string FileNameParameter = "FileName";
            public const string LanguageParameter = "Language";

            public static string GetPostFile(Guid id, Guid secondaryToken, string fileName, Language language)
            {
                var encodedName = HttpUtility.UrlEncode(fileName);
                var queryParameters = new NameValueCollection()
                    {
                    {FileIdParameter, id.ToString()},
                    {SecondaryTokenParameter, secondaryToken.ToString()},
                    {FileNameParameter, encodedName},
                    {LanguageParameter, (language).ToString()} };

                return RouteHelper.BuildRoutePartsWithParameters(queryParameters, "upload", nameof(GetPostFile));
            }
        }
    }
}
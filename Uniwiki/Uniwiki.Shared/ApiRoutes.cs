using System;
using System.Collections.Specialized;
using System.Web;
using Shared;

namespace Uniwiki.Shared
{
    public static class ApiRoutes
    {
        public static class FileController
        {
            public const string FileIdParameter = "FileId";
            public const string SecondaryTokenParameter = "Token";
            public const string FileNameParameter = "FileName";
            public const string LanguageParameter = "Language";

            public const string FileControllerRoute = "File";
            public static string GetPostFile(Guid id, Guid secondaryToken, string fileName, Language language)
            {
                var encodedName = HttpUtility.UrlEncode(fileName);
                var queryParameters = new NameValueCollection()
                    {
                    {FileIdParameter, id.ToString()},
                    {SecondaryTokenParameter, secondaryToken.ToString()},
                    {FileNameParameter, encodedName},
                    {LanguageParameter, language.ToString()} };

                return RouteHelper.BuildRoutePartsWithParameters(queryParameters, FileControllerRoute, nameof(GetPostFile));
            }

            public static class UploadFile
            {
                public static string BuildRoute() => "/" + FileControllerRoute;
            }
        }
    }
}
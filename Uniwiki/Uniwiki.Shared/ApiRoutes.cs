using System;
using System.Collections.Specialized;
using Shared;

namespace Uniwiki.Shared
{
    public static class ApiRoutes
    {
        public static class UploadController
        {
            public static string GetPostFile(Guid id, Guid secondaryToken, string fileName, Language language) => RouteHelper.BuildRoutePartsWithParameters(
                new NameValueCollection()
                {
                    {nameof(id), id.ToString()},
                    {"accessToken", secondaryToken.ToString()},
                    {nameof(fileName), fileName},
                    {nameof(language), ((int)language).ToString()}
        }, "upload", nameof(GetPostFile));
        }
    }
}

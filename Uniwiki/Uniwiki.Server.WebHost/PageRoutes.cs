using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uniwiki.Server.WebHost
{
    public static class PageRoutes
    {
        public static class AccountPage
        {
            public const string BaseRoute = "/Account";
            public const string RouteAttribute = BaseRoute;
            public static string BuildRoute() => BaseRoute;
        }

        public static class RegisterPage
        {
            public const string BaseRoute = "/Register";
            public const string RouteAttribute = BaseRoute;
            public static string BuildRoute() => BaseRoute;
        }

        public static class LoginPage
        {
            public const string BaseRoute = "/Login";
            public const string RouteAttribute = BaseRoute;
            public static string BuildRoute() => BaseRoute;
        }

        public static class HomePage
        {
            public const string BaseRoute = "/Home";
            public const string RouteAttribute = BaseRoute;
            public static string BuildRoute() => BaseRoute;
        }

        public static class UniversityPage
        {
            public const string BaseRoute = "/Uni";
            public const string UniversityRouteParameter = "universityUrl";
            public const string RouteAttribute = BaseRoute + "/{" + UniversityRouteParameter + "}";
            public static string BuildRoute(string universityUrl) => $"{BaseRoute}/{universityUrl}";
        }

        public static class FacultyPage
        {
            public const string BaseRoute = "/Uni";
            public const string UniversityRouteParameter = "universityUrl";
            public const string FacultyRouteParameter = "facultyUrl";
            public const string RouteAttribute = BaseRoute + "/{" + UniversityRouteParameter + "}/{" + FacultyRouteParameter + "}";
            public static string BuildRoute(string universityUrl, string facultyUrl) => $"{BaseRoute}/{universityUrl}/{facultyUrl}";
        }

        public static class CoursePage
        {
            public const string BaseRoute = "/Uni";
            public const string UniversityRouteParameter = "universityUrl";
            public const string FacultyRouteParameter = "facultyUrl";
            public const string CourseRouteParameter = "courseUrl";
            public const string RouteAttribute = BaseRoute + "/{" + UniversityRouteParameter + "}/{" + FacultyRouteParameter + "}/{" + CourseRouteParameter + "}";
            public static string BuildRoute(string universityUrl, string facultyUrl, string courseUrl) 
                => $"{BaseRoute}/{universityUrl}/{facultyUrl}/{courseUrl}";
        }

        public static class PostPage
        {
            public const string BaseRoute = "/Uni";
            public const string UniversityRouteParameter = "universityUrl";
            public const string FacultyRouteParameter = "facultyUrl";
            public const string CourseRouteParameter = "courseUrl";
            public const string PostRouteParameter = "postUrl";
            public const string RouteAttribute =
                BaseRoute + "/{" +
                UniversityRouteParameter + "}/{" +
                FacultyRouteParameter + "}/{" +
                CourseRouteParameter + "}/{" +
                PostRouteParameter + "}";
            public static string BuildRoute(string universityUrl, string facultyUrl, string courseUrl, string postUrl) 
                => $"{BaseRoute}/{universityUrl}/{facultyUrl}/{courseUrl}/{postUrl}";
        }

        public static class AddPostPage
        {
            public const string BaseRoute = "/AddPost";
            public const string UniversityRouteParameter = "universityUrl";
            public const string FacultyRouteParameter = "facultyUrl";
            public const string CourseRouteParameter = "courseUrl";
            public const string RouteAttribute =
                BaseRoute + "/{" +
                UniversityRouteParameter + "}/{" +
                FacultyRouteParameter + "}/{" +
                CourseRouteParameter + "}";
            public static string BuildRoute(string universityUrl, string facultyUrl, string courseUrl) 
                => $"{BaseRoute}/{universityUrl}/{facultyUrl}/{courseUrl}";
        }
    }
}

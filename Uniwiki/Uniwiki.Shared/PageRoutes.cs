using System.Collections.Specialized;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared
{
    public class PageRoutes
    {
        public static class HomePage
        {
            public static string BuildRoute() => RouteHelper.BuildRouteParts(BaseRoute);
            public const string BaseRoute = "Home";
        }

        public static class UniPage
        {
            public static string BuildRoute(string universityUrl) => RouteHelper.BuildRouteParts(BaseRoute, universityUrl);
            public const string BaseRoute = "Uni";
        }

        public static class ChooseCoursePage
        {
            public static string BuildRoute() => BaseRoute;
            public const string BaseRoute = "ChooseCourse";
        }

        public static class CoursePage
        {
            public static string BuildRoute(CourseDto course, string? postType = null)
            {
                return RouteHelper.BuildRouteParts(UniPage.BaseRoute, course.University.Url, course.StudyGroup.Url, course.Url, postType);
            }
        }

        public static class LoginPage
        {
            public static string BuildRoute() => BaseRoute;
            public const string BaseRoute = "Login";
        }

        public static class RegisterPage
        {
            public static string BuildRoute() => BaseRoute;
            public const string BaseRoute = "Register";
        }

        public static class ConfirmEmailPage
        {
            public static string BuildRoute(string email) => RouteHelper.BuildRouteParts(BaseRoute, email);

            public const string BaseRoute = "ConfirmEmail";
        }

        public static class EmailConfirmedPage
        {
            private static string EmailParameter = "Email";
            public static string BuildRoute(string secret, string email) => RouteHelper.BuildRoutePartsWithParameters(new NameValueCollection {{ EmailParameter, email}}, BaseRoute, secret);
            public const string BaseRoute = "EmailConfirmed";

            public static string? TryGetEmail(string url)
            {
                return RouteHelper.TryGetQueryParameterValue(url, EmailParameter);
            }
        }

        public static class ProfilePage
        {
            public static string BuildRoute(string nameIdentifier) => RouteHelper.BuildRouteParts(BaseRoute, nameIdentifier);
            public const string BaseRoute = "Profile";
        }


        public static class RestorePasswordPage
        {
            public static string BuildRoute() => BaseRoute;
            public const string BaseRoute = "RestorePassword";
        }

        public static class ChangePasswordPage
        {
            public static string BuildRoute() => BaseRoute;
            public const string BaseRoute = "ChangePassword";
        }
        public static class PasswordChangedPage
        {
            public static string BuildRoute() => BaseRoute;
            public const string BaseRoute = "PasswordChanged";
        }

        public static class RestorePasswordRequestedPage
        {
            public static string BuildRoute(string email) => RouteHelper.BuildRouteParts(BaseRoute, email);
            private const string BaseRoute = "RestorePasswordRequested";
        }

        public static class CreateNewPasswordPage
        {
            public static string BuildRoute(string email) => RouteHelper.BuildRouteParts("CreateNewPassword", email);
        }

        public static class AddCoursePage
        {
            public static string BuildRoute() => RouteHelper.BuildRouteParts(BaseRoute);
            private const string BaseRoute = "AddCourse";
        }

        public static class DownloadErrorPage
        {
            public static string BuildRoute() => RouteHelper.BuildRouteParts("/DownloadError");
        }

        public static class DownloadTimeErrorPage
        {
            public static string BuildRoute() => RouteHelper.BuildRouteParts("/DownloadTimeError");
        }

        public static class FileNotFoundErrorPage
        {
            public static string BuildRoute() => RouteHelper.BuildRouteParts("/FileNotFoundError");
        }

        public static class TermsOfUsePage
        {
            public static string BuildRoute() => RouteHelper.BuildRouteParts("/TermsOfUse");
        }

    }
}
 
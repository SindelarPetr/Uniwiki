namespace Uniwiki.Server.Application.Extensions
{
    public static class StringExtensions
    {
        public static string StandardizeEmail(this string email) => email.Trim().ToLower();
    }
}

using System;
using Shared;

namespace Uniwiki.Shared
{
    public static class Constants
    {
        /// <summary>
        /// Determines how much time the user has to wait in order for him to be possible to have the confirmation email resent.
        /// </summary>
        public static TimeSpan ResendRegistrationEmailMinTime => TimeSpan.FromSeconds(30);

        public static TimeSpan RestorePasswordSecretExpiration => TimeSpan.FromHours(5);
        public static TimeSpan RestorePasswordSecretPause => TimeSpan.FromSeconds(30);
        public static TimeSpan LoginTokenLife => TimeSpan.FromDays(60);
        
        public static TimeSpan DownloadAgainTime = TimeSpan.FromSeconds(10);

        public const string FacebookLink = "https://www.facebook.com/UniwikiOfficial";

        public const int NumberOrRecentCourses = 6;

        public const string FileUploadDataField = "Data";

#if DEBUG
        public const long MaxFileSizeInBytes = 50_000_000;
#else
        public const long MaxFileSizeInBytes = 50_000_000;
#endif

        public const Language DefaultLanguage = Language.Czech;

        public const int MaxPostsToFetch = 15;

        public static class Validations
        {
            public const int CourseCodeMaxLength = 10;
            public const int CourseCodeMinLength = 0;
            public const int CourseNameMaxLength = 95;
            public const int CourseNameMinLength = 3;

            public const int StudyGroupNameMaxLength = 70;
            public const int StudyGroupShortcutMaxLength = 10;
            public const int StudyGroupNameMinLength = 4;
            public const int StudyGroupShortcutMinLength = 1;
            public const int PostTextMaxLength = 50_000;
            public const int PostTextMinLength = 1;

            public static readonly char[] AllowedFileSpecialCharacters = {'<', '>', '(', ')', '.', ' ', '[', ']', '{', '}', '-', '=', '*', '!', '@', '#', '%', '+', '_', ',', '$' };
            public const int FileNameMaxLength = 240;
            public const int PostTypeMaxLength = 25;

            public const int UserNameMinLength = 2;
            public const int UserNameMaxLength = 15;
            public const int UserSurnameMinLength = 2;
            public const int UserSurnameMaxLength = 20;
        }
    }
}

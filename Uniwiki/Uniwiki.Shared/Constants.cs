﻿using System;
using Shared;
using Shared.Services.Abstractions;

namespace Uniwiki.Shared
{
    public class Constants
    {
        public Constants(IApplicationHostEnvironment applicationHostEnvironment)
        {
            _applicationHostEnvironment = applicationHostEnvironment;
        }

        /// <summary>
        /// Determines how much time the user has to wait in order for him to be possible to have the confirmation email resent.
        /// </summary>
        public static TimeSpan ResendRegistrationEmailMinTime => TimeSpan.FromSeconds(30);

        public static TimeSpan RestorePasswordSecretExpiration => TimeSpan.FromHours(5);
        public static TimeSpan RestorePasswordSecretPause => TimeSpan.FromSeconds(30);
        public static TimeSpan LoginTokenLife => TimeSpan.FromDays(60);
        
        public static TimeSpan DownloadAgainTime = TimeSpan.FromSeconds(10);

        public const string FacebookLink = "https://www.facebook.com/UniwikiOfficial";

        public const int NumberOrRecentCourses = 4;

        public const string FileUploadDataField = "Data";

#if DEBUG
        public const long MaxFileSizeInBytes = 50_000_000;
#else
        public const long MaxFileSizeInBytes = 50_000_000;
#endif

        public const Language DefaultLanguage = Language.English;

        public const int MaxPostsToFetch = 15;

        private readonly IApplicationHostEnvironment _applicationHostEnvironment;

        public int PasswordMinLength => _applicationHostEnvironment.IsProduction() ? 6 : 1; // Require 6 digit password just for production
        
        public class Validations
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

            public readonly static char[] AllowedFileSpecialCharacters = {'<', '>', '(', ')', '.', ' ', '[', ']', '{', '}', '-', '=', '*', '!', '@', '#', '%', '+', '_', ',', '$' };
            public const int FileNameMaxLength = 240;
            public const int FileExtensionMaxLength = 20;
            public const int PostTypeMaxLength = 25;

            public const int UserNameAndSurnameMinLength = 3;
            public const int UserNameMaxLength = 35;
            public const int UserSurnameMaxLength = 35;
            public const int UserNameAndSurnameMaxLength = UserNameMaxLength + 1 + UserSurnameMaxLength;

            public const int UrlMaxLength = 200;
            public const int EmailMaxLength = 254;
            public const int PasswordMaxLength = 128;
            public const int UniversityLongName = 100;
            public const int UniversityShortName = 15;
        }
    }
}

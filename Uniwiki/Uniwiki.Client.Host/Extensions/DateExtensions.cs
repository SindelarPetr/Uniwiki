using Shared.Services.Abstractions;
using System;
using Uniwiki.Client.Host.Services;

namespace Uniwiki.Client.Host.Extensions
{
    internal static class DateExtensions
    {
        public static string ToPassedTimeString(this DateTime dateTime, ITimeService timeService, TextService textService)
        {
            var timeDifference = timeService.Now - dateTime;

            var years = timeDifference.Days / 365;
            if (years >= 1)
            {
                return textService.PassedTime_Years(years);
            }

            var weeks = timeDifference.Days / 7;
            if (weeks >= 1)
            {
                return textService.PassedTime_Weeks(weeks);
            }

            if (timeDifference.Days >= 1)
            {
                return textService.PassedTime_Days(timeDifference.Days);
            }

            if (timeDifference.Hours >= 1)
            {
                return textService.PassedTime_Hours(timeDifference.Hours);
            }

            if (timeDifference.Minutes >= 1)
            {
                return textService.PassedTime_Minutes(timeDifference.Minutes);
            }

            return textService.PassedTime_Now;
        }
    }
}

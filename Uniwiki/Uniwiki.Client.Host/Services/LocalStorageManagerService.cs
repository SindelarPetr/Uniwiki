using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using Shared;
using Uniwiki.Client.Host.Services.Abstractions;
using Uniwiki.Shared;
using Uniwiki.Shared.ModelDtos;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Client.Host.Services
{
    public class LocalStorageManagerService : ILocalStorageManagerService
    {
        private readonly ILocalStorageService _localStorageService;

        public LocalStorageManagerService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        private readonly string _languageKey = "Language";
        public async Task<Language?> GetCurrentLanguage()
        {
            var stringLang = await GetItemOrDefaultAsync(_languageKey, string.Empty) ?? string.Empty;

            if (!int.TryParse(stringLang, out var intLang))
            {
                return null;
            }

            if (!Enum.IsDefined(typeof(Language), intLang))
                return null;

            var lang = (Language)intLang;

            return lang;
        }
        public Task SetCurrentLanguage(Language language) => _localStorageService.SetItemAsync(_languageKey, language);

        private readonly string _loginTokenKey = "LoginToken";
        public Task<LoginTokenDto?> GetLoginToken() => GetItemOrDefaultAsync<LoginTokenDto?>(_loginTokenKey, null);
        public Task SetLoginToken(LoginTokenDto loginToken) => _localStorageService.SetItemAsync(_loginTokenKey, loginToken);
        public Task RemoveLoginToken() => _localStorageService.RemoveItemAsync(_loginTokenKey);

        private readonly string _loginProfileKey = "LoginProfile";
        public async Task<ProfileDto?> GetLoginProfile() {

            var containsProfile = await _localStorageService.ContainKeyAsync(_loginProfileKey);

            if (containsProfile)
            {
                return await GetItemOrDefaultAsync<ProfileDto?>(_loginProfileKey, null);
            }
            else
            {
                return null;
            }
        }
        public Task SetLoginProfile(ProfileDto loginProfile) => _localStorageService.SetItemAsync(_loginProfileKey, loginProfile);
        public Task RemoveLoginProfile() => _localStorageService.RemoveItemAsync(_loginProfileKey);

        private async Task<T> GetItemOrDefaultAsync<T>(string key, T defaultT)
        {
            try
            {
                return await _localStorageService.GetItemAsync<T>(key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return defaultT;
            }
        }

        private const string RecentCoursesKey = "RecentCourses";
        public Task SetRecentCourses(FoundCourseDto[] courses)
        {
            // Save it to the storage
            return _localStorageService.SetItemAsync(RecentCoursesKey, courses);
        }

        public async Task SetRecentCourse(FoundCourseDto course)
        {
            // Get recent courses first
            var recentCourses = (await GetRecentCourses()).ToList();

            // Remove the course in case its there already. Use Url for mapping the courses, that is better for
            recentCourses.RemoveAll(c => c.FullUrl == course.FullUrl);

            // Add to recent courses again
            recentCourses.Add(course);

            // Take only the last X elements
            var recentCoursesArray = recentCourses.TakeLast(Constants.NumberOrRecentCourses).ToArray();

            // Serialize courses
            var serializedRecentCourses = JsonConvert.SerializeObject(recentCoursesArray);

            // Save it to the storage
            await _localStorageService.SetItemAsync(RecentCoursesKey, serializedRecentCourses);
        }

        public async Task<FoundCourseDto[]> GetRecentCourses()
        {
            var serializedCourses = await GetItemOrDefaultAsync(RecentCoursesKey, string.Empty);

            try
            {
                // Deserialize courses or return empty array
                return string.IsNullOrWhiteSpace(serializedCourses) ? new FoundCourseDto[0] : JsonConvert.DeserializeObject<FoundCourseDto[]>(serializedCourses);
            }
            catch (Exception)
            {
                // Return empty array if there was a problem
                return new FoundCourseDto[0];
            }

        }

        private const string FeedbackProvidedKey = "FeedbackProvided";
        public Task<bool> IsFeedbackProvided() => GetItemOrDefaultAsync(FeedbackProvidedKey, false);
        public Task SetFeedbackProvided() => _localStorageService.SetItemAsync(FeedbackProvidedKey, true);
    }
}

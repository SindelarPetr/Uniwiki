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
            var stringLang = await SafeGetItemAsync<string>(_languageKey) ?? string.Empty;

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
        public Task<LoginTokenDto?> GetLoginToken() => SafeGetItemAsync<LoginTokenDto?>(_loginTokenKey);
        public Task SetLoginToken(LoginTokenDto loginToken) => _localStorageService.SetItemAsync(_loginTokenKey, loginToken);
        public Task RemoveLoginToken() => _localStorageService.RemoveItemAsync(_loginTokenKey);

        private readonly string _loginProfileKey = "LoginProfile";
        public async Task<ProfileDto?> GetLoginProfile() {

            var containsProfile = await _localStorageService.ContainKeyAsync(_loginProfileKey);

            if (containsProfile)
            {
                return await SafeGetItemAsync<ProfileDto?>(_loginProfileKey);
            }
            else
            {
                return null;
            }
        }
        public Task SetLoginProfile(ProfileDto loginProfile) => _localStorageService.SetItemAsync(_loginProfileKey, loginProfile);
        public Task RemoveLoginProfile() => _localStorageService.RemoveItemAsync(_loginProfileKey);

        private async Task<T> SafeGetItemAsync<T>(string key)
        {
            try
            {
                return await _localStorageService.GetItemAsync<T>(key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return default;
            }
        }

        private const string RecentCoursesKey = "RecentCourses";
        public async Task SetRecentCourses(CourseDto[] courses)
        {
            // Save it to the storage
            await _localStorageService.SetItemAsync(RecentCoursesKey, courses);
        }

        public async Task SetRecentCourse(CourseDto course)
        {
            // Get recent courses first
            var recentCourses = (await GetRecentCourses()).ToList();

            // Remove the course in case its there already. Use Url for mapping the courses, that is better for
            recentCourses.RemoveAll(c => c.Url == course.Url && c.StudyGroup.Url == course.StudyGroup.Url && c.University.Url == course.University.Url);

            // Add to recent courses again
            recentCourses.Add(course);

            // Take only the last X elements
            var recentCoursesArray = recentCourses.TakeLast(Constants.NumberOrRecentCourses).ToArray();

            // Serialize courses
            var serializedRecentCourses = JsonConvert.SerializeObject(recentCoursesArray);

            // Save it to the storage
            await _localStorageService.SetItemAsync(RecentCoursesKey, serializedRecentCourses);
        }

        public async Task<CourseDto[]> GetRecentCourses()
        {
            // Check if the courses are saved there
            var hasCourses = await _localStorageService.ContainKeyAsync(RecentCoursesKey);

            Console.WriteLine("HasCourses: " + hasCourses);

            // Load them if they are there
            if (hasCourses)
            { 
                var serializedCourses = await SafeGetItemAsync<string>(RecentCoursesKey);

                // Deserialize courses
                var courses = JsonConvert.DeserializeObject<CourseDto[]>(serializedCourses);

                return courses;
            }
            else
                return new CourseDto[0];

        }
    }
}

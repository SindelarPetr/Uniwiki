using System;
using System.Linq;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.InMemory.Services;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Models.Authentication;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Server.Persistence.Services;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Server.Persistence.InMemory.Repositories.Authentication
{
    internal class ProfileInMemoryRepository : IProfileRepository
    {
        private readonly DataService _dataStorage;
        private readonly TextService _textService;

        public ProfileInMemoryRepository(DataService dataStorage, TextService textService)
        {
            _dataStorage = dataStorage;
            _textService = textService;
        }

        public ProfileModel Register(string email, string name, string surname, string url, string password, byte[] passwordSalt, DateTime registrationTime)
        {
            if (_dataStorage.Profiles.Any(p => p.Email == email))
                throw new RequestException(_textService.Error_EmailIsAlreadyTaken(email));

            var id = Guid.NewGuid();
            var recentCourses = _dataStorage.CourseVisits.Where(cv => cv.Profile.Id == id)
                .OrderByDescending(cv => cv.VisitDateTime).Select(cv => cv.Course).Distinct().Reverse();

            var newProfile = new ProfileModel(
                id, 
                email, 
                name, 
                surname, 
                url, 
                password,
                passwordSalt, 
                $"/img/profilePictures/no-profile-picture.jpg", 
                registrationTime, 
                false, 
                AuthenticationLevel.PrimaryToken, 
                recentCourses);

            _dataStorage.Profiles.Add(newProfile);

            return newProfile;
        }

        public ProfileModel GetProfileByUrl(string url)
        {
            return _dataStorage.Profiles.FirstOrDefault(p => p.Url == url) ??
                   throw new RequestException(_textService.Error_UserNotFound);
        }

        public ProfileModel GetProfileByEmail(string email) => TryGetProfileByEmail(email) ?? throw new RequestException(_textService.Error_NoUserWithProvidedEmail(email));

        public ProfileModel TryGetProfileByEmail(string email) =>
            _dataStorage.Profiles.FirstOrDefault(p => p.Email == email);

        public void ChangePassword(ProfileModel profile, string newPassword, byte[] passwordSalt)
        {
            profile.ChangePassword(newPassword, passwordSalt);
        }

        public ProfileModel FindById(Guid id)
        {
            return _dataStorage.Profiles.FirstOrDefault(p => p.Id == id) ?? throw new RequestException(_textService.Error_UserNotFound);
        }

        public ProfileModel TryGetProfileByUrl(string url)
        {
            return _dataStorage.Profiles.FirstOrDefault(p => p.Url == url);

        }

        public void SetAdmin(ProfileModel profile)
        {
            profile.SetAuthenticationLevel(AuthenticationLevel.Admin);
        }
    }
}

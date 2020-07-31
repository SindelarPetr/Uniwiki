using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Models.Authentication;

namespace Uniwiki.Server.Persistence.Repositories.Authentication
{
    public interface IProfileRepository
    {
        ProfileModel Register(string email, string name, string surname, string url, string password, byte[] passwordSalt, DateTime registrationTime, StudyGroupModel? homeFaculty);
        ProfileModel GetProfileByUrl(string url);
        ProfileModel GetProfileByEmail(string email);
        ProfileModel TryGetProfileByEmail(string email);
        void ChangePassword(ProfileModel profile, string newPassword, byte[] passwordSalt);
        ProfileModel FindById(Guid id);
        ProfileModel TryGetProfileByUrl(string url);
        public void SetAdmin(ProfileModel profile);
        void EditHomeFaculty(ProfileModel user, StudyGroupModel? homeFaculty);
    }
}
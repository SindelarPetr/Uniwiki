using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface IProfileRepository : IIdRepository<ProfileModel, Guid>
    {
        //ProfileModel Register(string email, string name, string surname, string url, string password, byte[] passwordSalt, DateTime registrationTime, StudyGroupModel? homeFaculty);
        ProfileModel GetProfileByUrl(string url);
        ProfileModel GetProfileByEmail(string email);
        ProfileModel TryGetProfileByEmail(string email);
        void ChangePassword(ProfileModel profile, string newPassword, byte[] passwordSalt);
        ProfileModel TryGetProfileByUrl(string url);
        void SetAdmin(ProfileModel profile);
        void EditHomeFaculty(ProfileModel user, StudyGroupModel? homeFaculty);
    }
}
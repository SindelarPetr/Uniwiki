using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    public class ProfileRepository : RepositoryBase<ProfileModel, Guid> 
    {
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.Error_UserNotFound;

        public ProfileRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.Profiles)
        {
            _textService = textService;
        }

        public ProfileModel GetProfileByUrl(string url) 
            => All.FirstOrDefault(p => p.Url == url) ??
            throw new RequestException(_textService.Error_UserNotFound);

        public ProfileModel GetProfileByEmail(string email) 
            => TryGetProfileByEmail(email) ?? throw new RequestException(_textService.Error_NoUserWithProvidedEmail(email));

        public ProfileModel TryGetProfileByEmail(string email) 
            => All.FirstOrDefault(p => p.Email == email);

        public void ChangePassword(ProfileModel profile, string newPassword, byte[] passwordSalt) 
            => profile.ChangePassword(newPassword, passwordSalt);

        public ProfileModel TryGetProfileByUrl(string url) 
            => All.FirstOrDefault(p => p.Url == url);

        public ProfileModel AddProfile(string email, string firstName, string familyName, string url, string hashedPassword, byte[] salt, string profilePictureSrc, DateTime creationTime, bool isConfirmed, AuthenticationLevel authenticationLevel, Guid? homeFacultyId)
        {
            var profile = new ProfileModel(Guid.NewGuid(), email, firstName, familyName, url, hashedPassword, salt, profilePictureSrc, creationTime, isConfirmed, authenticationLevel, homeFacultyId);

            All.Add(profile);

            SaveChanges();

            return profile;
        }

        public IQueryable<ProfileModel> EditHomeFaculty(Guid profileId, Guid? facultyId)
        {
            var profile = UniwikiContext.Profiles.Single(p => p.Id == profileId);

            profile.SetHomeFaculty(facultyId);

            return FindById(profileId);
        }

        public void SetAsAdmin(ProfileModel profile) => profile.SetAuthenticationLevel(AuthenticationLevel.Admin);
    }
}

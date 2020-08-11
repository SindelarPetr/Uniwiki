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
    internal class ProfileRepository : RepositoryBase<ProfileModel>, IProfileRepository
    {
        private readonly TextService _textService;

        public string NotFoundByIdMessage => _textService.Error_UserNotFound;

        public ProfileRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.Profiles)
        {
            _textService = textService;
        }

        public ProfileModel GetProfileByUrl(string url)
        {
            return All.FirstOrDefault(p => p.Url == url) ??
                   throw new RequestException(_textService.Error_UserNotFound);
        }

        public ProfileModel GetProfileByEmail(string email) => TryGetProfileByEmail(email) ?? throw new RequestException(_textService.Error_NoUserWithProvidedEmail(email));

        public ProfileModel TryGetProfileByEmail(string email) =>
            All.FirstOrDefault(p => p.Email == email);

        public void ChangePassword(ProfileModel profile, string newPassword, byte[] passwordSalt)
        {
            profile.ChangePassword(newPassword, passwordSalt);
        }

        //public ProfileModel FindById(Guid id)
        //{
        //    return All.FirstOrDefault(p => p.Id == id) ?? throw new RequestException(_textService.Error_UserNotFound);
        //}

        public ProfileModel TryGetProfileByUrl(string url)
        {
            return All.FirstOrDefault(p => p.Url == url);

        }

        public void SetAdmin(ProfileModel profile)
        {
            profile.SetAuthenticationLevel(AuthenticationLevel.Admin);
        }

        public void EditHomeFaculty(ProfileModel user, StudyGroupModel? homeFaculty)
        {
            user.SetHomeFaculty(homeFaculty);
        }
    }
}

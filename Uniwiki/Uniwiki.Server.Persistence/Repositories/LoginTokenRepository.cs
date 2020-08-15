using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class LoginTokenRepository : RepositoryBase<LoginTokenModel, Guid>, ILoginTokenRepository
    {
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.Error_LoginTokenNotFound;

        public LoginTokenRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.LoginTokens)
        {
            _textService = textService;
        }


        public LoginTokenModel? TryFindNonExpiredById(Guid tokenValue, DateTime searchTime)
        {
            return All.FirstOrDefault(t => (t.PrimaryTokenId == tokenValue || t.SecondaryTokenId == tokenValue) && t.Expiration > searchTime);
        }

        public LoginTokenModel AddLoginToken(Guid primaryToken, Guid secondaryToken, ProfileModel profile, DateTime creationTime, DateTime expiration)
        {
            var loginToken = new LoginTokenModel(Guid.NewGuid(), primaryToken, secondaryToken, profile, creationTime, expiration);

            All.Add(loginToken);

            SaveChanges();

            return loginToken;
        }
    }
}

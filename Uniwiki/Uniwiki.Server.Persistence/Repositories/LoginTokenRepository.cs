using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class LoginTokenRepository : RepositoryBase<LoginTokenModel>, ILoginTokenRepository
    {
        private readonly TextService _textService;

        public string NotFoundByIdMessage => _textService.Error_LoginTokenNotFound;

        public LoginTokenRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.LoginTokens)
        {
            _textService = textService;
        }


        public LoginTokenModel? TryFindNonExpiredById(Guid tokenValue, DateTime searchTime)
        {
            return All.FirstOrDefault(t => (t.PrimaryTokenId == tokenValue || t.SecondaryTokenId == tokenValue) && t.Expiration > searchTime);
        }
    }
}

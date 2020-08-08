using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class LoginTokenRepository : ILoginTokenRepository
    {
        private readonly UniwikiContext _dataStorage;

        public LoginTokenRepository(UniwikiContext dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public LoginTokenModel IssueLoginToken(ProfileModel profile, DateTime creationTime, DateTime expiration)
        {
            // Issue a new token - pair it with the current user
            var token = new LoginTokenModel(Guid.NewGuid(), profile, creationTime, expiration, Guid.NewGuid());

            // Persist the token
            _dataStorage.LoginTokens.Add(token);

            return token;
        }

        public LoginTokenModel? TryFindById(Guid tokenValue, DateTime searchTime)
        {
            return _dataStorage.LoginTokens.FirstOrDefault(t => (t.PrimaryTokenId == tokenValue || t.SecondaryTokenId == tokenValue) && t.Expiration > searchTime);
        }
    }
}

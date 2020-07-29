using System;
using System.Linq;
using Uniwiki.Server.Persistence.InMemory.Services;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Models.Authentication;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared;

namespace Uniwiki.Server.Persistence.InMemory.Repositories.Authentication
{
    internal class LoginTokenInMemoryRepository : ILoginTokenRepository
    {
        private readonly DataService _dataStorage;

        public LoginTokenInMemoryRepository(DataService dataStorage)
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

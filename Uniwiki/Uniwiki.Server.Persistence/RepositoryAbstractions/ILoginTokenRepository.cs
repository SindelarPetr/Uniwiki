using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface ILoginTokenRepository : IIdRepository<LoginTokenModel, Guid>
    {
        //LoginTokenModel IssueLoginToken(ProfileModel profile, DateTime creationTime, DateTime expiration);
        LoginTokenModel? TryFindById(Guid token, DateTime searchTime);
    }
}
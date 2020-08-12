using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface ILoginTokenRepository : IIdRepository<LoginTokenModel, Guid>
    {
        LoginTokenModel? TryFindNonExpiredById(Guid token, DateTime searchTime);
        LoginTokenModel AddLoginToken(Guid primaryToken, Guid secondaryToken, ProfileModel profile, DateTime creationTime, DateTime extendedExpiration);
    }
}
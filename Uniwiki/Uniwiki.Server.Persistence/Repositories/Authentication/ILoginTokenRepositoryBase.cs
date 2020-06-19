using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Models.Authentication;

namespace Uniwiki.Server.Persistence.Repositories.Authentication
{
    public interface ILoginTokenRepository
    {
        LoginTokenModel IssueLoginToken(ProfileModel profile, DateTime creationTime);
        LoginTokenModel? TryFindById(Guid token, DateTime searchTime);
    }
}
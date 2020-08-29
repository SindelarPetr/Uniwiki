using System;
using System.Linq;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Application.Services.Abstractions
{
    internal interface ILoginService
    {
        IQueryable<LoginTokenModel> LoginUser(string email, string password);
        IQueryable<LoginTokenModel> LoginUser(Guid profileId);
    }
}
using Shared.Services.Abstractions;
using System;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;

namespace Uniwiki.Server.Application.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly ILoginTokenRepository _loginTokenRepository;
        private readonly ITimeService _timeService;

        public AuthenticationService(ILoginTokenRepository loginTokenRepository, ITimeService timeService)
        {
            _loginTokenRepository = loginTokenRepository;
            _timeService = timeService;
        }

        public (LoginTokenModel?, AuthenticationLevel) TryAuthenticate(Guid? tokenId)
        {
            if (tokenId != null)
            {
                // Get token guid
                var token = _loginTokenRepository.TryFindNonExpiredById(tokenId.Value, _timeService.Now);

                if (token != null)
                {
                    var authenticationLevel = tokenId == token.PrimaryTokenId
                        ? AuthenticationLevel.PrimaryToken
                        : AuthenticationLevel.SecondaryToken;

                    // If the user is admin, then set level to Admin
                    if (token.Profile.AuthenticationLevel == AuthenticationLevel.Admin)
                        authenticationLevel = AuthenticationLevel.Admin;

                    return (token, authenticationLevel);
                }
            }

            return (null, AuthenticationLevel.None);
        }
    }
}
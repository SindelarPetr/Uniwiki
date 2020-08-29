using Shared.Exceptions;
using Shared.Services.Abstractions;
using System;
using System.Linq;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared;

namespace Uniwiki.Server.Application.Services
{
    internal class LoginService : ILoginService
    {
        private readonly ProfileRepository _profileRepository;
        private readonly IHashService _hashService;
        private readonly TextService _textService;
        private readonly ITimeService _timeService;
        private readonly LoginTokenRepository _loginTokenRepository;
        private readonly IInputValidationService _inputValidationService;
        private readonly UniwikiContext _uniwikiContext;

        public LoginService(ProfileRepository profileRepository, IHashService hashService, TextService textService, ITimeService timeService, LoginTokenRepository loginTokenRepository, IInputValidationService inputValidationService, UniwikiContext uniwikiContext)
        {
            _profileRepository = profileRepository;
            _hashService = hashService;
            _textService = textService;
            _timeService = timeService;
            _loginTokenRepository = loginTokenRepository;
            _inputValidationService = inputValidationService;
            _uniwikiContext = uniwikiContext;
        }

        public IQueryable<LoginTokenModel> LoginUser(string email, string password)
        {
            // Standardize email
            email = email.StandardizeEmail();

            // Validate email
            _inputValidationService.ValidateEmail(email);

            // Get profile
            var profile = _profileRepository.GetProfileByEmail(email);

            // Hash the password
            var hashedPassword = _hashService.HashPassword(password, profile.PasswordSalt);

            // Validate password, throw exception if not matching
            if (profile.Password != hashedPassword)
                throw new RequestException(_textService.Error_WrongLoginCredentials);

            return LoginUserInner(profile);
        }

        public IQueryable<LoginTokenModel> LoginUser(Guid profileId)
        {
            // Find the user in the DB
            var profile = _uniwikiContext.Profiles.Single(p => p.Id == profileId);

            return LoginUserInner(profile);
        }

        private IQueryable<LoginTokenModel> LoginUserInner(ProfileModel profile)
        {
            // Validate profile confirmation
            if (!profile.IsConfirmed)
                throw new RequestException(_textService.Error_YourEmailWasNotYetConfirmed(profile.Email));

            // Get the creation time
            var creationTime = _timeService.Now;

            // Calculate the expiration of the token
            var expiration = creationTime.Add(Constants.LoginTokenLife);

            // Extend the expiration of the token for 3 years for now - to avoid renewals
            var extendedExpiration = expiration.AddYears(3);

            // Create the token
            var token = _loginTokenRepository.AddLoginToken(Guid.NewGuid(), Guid.NewGuid(), profile.Id, creationTime, extendedExpiration);

            return _uniwikiContext.LoginTokens.Where(t => t.Id == token.Id);
        }
    }
}

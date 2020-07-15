using Shared.Exceptions;
using Shared.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services.Abstractions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Models.Authentication;
using Uniwiki.Server.Persistence.Repositories.Authentication;

namespace Uniwiki.Server.Application.Services
{
    internal class LoginService : ILoginService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IHashService _hashService;
        private readonly TextService _textService;
        private readonly ITimeService _timeService;
        private readonly ILoginTokenRepository _loginTokenRepository;
        private readonly IInputValidationService _inputValidationService;

        public LoginService(IProfileRepository profileRepository, IHashService hashService, TextService textService, ITimeService timeService, ILoginTokenRepository loginTokenRepository, IInputValidationService inputValidationService)
        {
            _profileRepository = profileRepository;
            _hashService = hashService;
            _textService = textService;
            _timeService = timeService;
            _loginTokenRepository = loginTokenRepository;
            _inputValidationService = inputValidationService;
        }

        public LoginTokenModel LoginUser(string email, string password)
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

            return LoginUser(profile);
        }

        public LoginTokenModel LoginUser(ProfileModel profile)
        {
            // Validate profile confirmation
            if (!profile.IsConfirmed)
                throw new RequestException(_textService.Error_YourEmailWasNotYetConfirmed(profile.Email));

            // Issue the token
            var token = _loginTokenRepository.IssueLoginToken(profile, _timeService.Now);

            return token;
        }
    }
}

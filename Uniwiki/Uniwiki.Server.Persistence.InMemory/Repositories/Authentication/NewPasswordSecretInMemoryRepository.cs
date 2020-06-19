using System;
using System.Linq;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.InMemory.Services;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Models.Authentication;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.InMemory.Repositories.Authentication
{
    internal class NewPasswordSecretInMemoryRepository : INewPasswordSecretRepository
    {
        private readonly DataService _dataStorage;
        private readonly TextService _textService;

        public NewPasswordSecretInMemoryRepository(DataService dataStorage, TextService textService)
        {
            _dataStorage = dataStorage;
            _textService = textService;
        }

        public NewPasswordSecretModel GenerateNewPasswordSecret(ProfileModel profile, DateTime creationTime, DateTime expirationTime)
        {
            // Generate secret
            var secret = Guid.NewGuid();

            // Generate model
            var newPasswordSecret = new NewPasswordSecretModel(profile, secret, creationTime, expirationTime, true);

            // Add it to the DB
            _dataStorage.NewPasswordSecrets.Add(newPasswordSecret);

            return newPasswordSecret;
        }

        public ProfileModel GetProfileForNewPasswordSecret(Guid secret) => _dataStorage.NewPasswordSecrets.FirstOrDefault(s => s.Secret == secret && s.IsValid)?.Profile ?? throw new RequestException(_textService.Error_FailedToCreateTheNewPassword);
        
        public NewPasswordSecretModel? TryGetSecretForProfile(ProfileModel profile)
        {
            return _dataStorage.NewPasswordSecrets.FirstOrDefault(s => s.Profile == profile);
        }

        public void InvalidateSecrets(ProfileModel profile)
        {
            foreach (var secret in _dataStorage.NewPasswordSecrets.Where(s => s.Profile == profile && s.IsValid))
            {
                secret.Invalidate();
            }
        }

        public NewPasswordSecretModel GetValidSecretById(Guid secret)
        {
            return _dataStorage.NewPasswordSecrets.FirstOrDefault(s => s.Secret == secret && s.IsValid) ??
                throw new RequestException( _textService.Error_FailedToCreateTheNewPassword);
        }
    }
}

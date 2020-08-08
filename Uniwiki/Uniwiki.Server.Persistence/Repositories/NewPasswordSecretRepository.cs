using System;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class NewPasswordSecretRepository : INewPasswordSecretRepository
    {
        private readonly UniwikiContext _dataStorage;
        private readonly TextService _textService;

        public NewPasswordSecretRepository(UniwikiContext dataStorage, TextService textService)
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

using System;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class EmailConfirmationSecretRepository : IEmailConfirmationSecretRepository
    {
        private readonly UniwikiContext _dataStorage;
        private readonly TextService _textService;

        public EmailConfirmationSecretRepository(UniwikiContext dataStorage, TextService textService)
        {
            _dataStorage = dataStorage;
            _textService = textService;
        }

        public EmailConfirmationSecretModel GenerateEmailConfirmationSecret(ProfileModel profile, DateTime creationTime)
        {
            var secretGuid = Guid.NewGuid();
            var secret = new EmailConfirmationSecretModel(profile, secretGuid, creationTime, true);
            
            return secret;
        }

        public void SaveEmailConfirmationSecret(EmailConfirmationSecretModel emailConfirmationSecret)
        {
            _dataStorage.EmailConfirmationSecrets.Add(emailConfirmationSecret);
        }

        public void ConfirmEmail(EmailConfirmationSecretModel secret)
        {
            var profile = secret.Profile;
            profile.SetAsConfirmed();
        }

        public void InvalidateSecrets(ProfileModel profile)
        {
            foreach (var emailConfirmationSecretModel in _dataStorage.EmailConfirmationSecrets.Where(s => s.Profile == profile && s.IsValid))
            {
                emailConfirmationSecretModel.Invalidate();
            }
        }

        public EmailConfirmationSecretModel? TryGetValidEmailConfirmationSecret(ProfileModel profile)
        {
            return _dataStorage.EmailConfirmationSecrets.FirstOrDefault(s => s.Profile == profile && s.IsValid);
        }

        public EmailConfirmationSecretModel FindValidById(Guid secret)
        {
            return _dataStorage.EmailConfirmationSecrets.FirstOrDefault(s => s.Secret == secret && s.IsValid) 
                   ?? throw new RequestException(_textService.Error_EmailConfirmationFailed);
        }

        public EmailConfirmationSecretModel FindById(Guid secret)
        {
            return _dataStorage.EmailConfirmationSecrets.FirstOrDefault(s => s.Secret == secret)
                   ?? throw new RequestException(_textService.Error_EmailConfirmationFailed);
        }
    }
}

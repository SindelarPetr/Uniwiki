using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;

using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    public class NewPasswordSecretRepository : RepositoryBase<NewPasswordSecretModel, Guid>
    {
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.Error_NewPasswordSecretNotFound;

        public NewPasswordSecretRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.NewPasswordSecrets)
        {
            _textService = textService;
        }

        public ProfileModel GetProfileForNewPasswordSecret(Guid secret) 
            => All.FirstOrDefault(s => s.Secret == secret && s.IsValid)?.Profile 
               ?? throw new RequestException(_textService.Error_FailedToCreateTheNewPassword);
        
        public NewPasswordSecretModel? TryGetSecretForProfile(Guid profileId) 
            => All.FirstOrDefault(s => s.ProfileId == profileId);

        public void InvalidateSecrets(Guid profileId)
        {
            foreach (var secret in All.Where(s => s.ProfileId == profileId && s.IsValid))
            {
                secret.Invalidate();
            }
        }

        public NewPasswordSecretModel GetValidSecret(Guid secret) 
            => All.FirstOrDefault(s => s.Secret == secret && s.IsValid) ??
            throw new RequestException( _textService.Error_FailedToCreateTheNewPassword);

        public NewPasswordSecretModel AddNewPasswordSecret(Guid profileId, Guid secret, DateTime creationTime, DateTime expirationTime)
        {
            var newPasswordSecretModel = new NewPasswordSecretModel(Guid.NewGuid(), profileId, secret, creationTime, expirationTime, true);

            All.Add(newPasswordSecretModel);

            SaveChanges();

            return newPasswordSecretModel;
        }
    }
}

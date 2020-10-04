using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;

using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    public class EmailConfirmationSecretRepository //: RepositoryBase<EmailConfirmationSecretModel, Guid> //, EmailConfirmationSecretRepository
    {
        private readonly UniwikiContext _uniwikiContext;
        private readonly TextService _textService;

        //public override string NotFoundByIdMessage => _textService.EmailConfirmationSecretNotFound;

        public EmailConfirmationSecretRepository(UniwikiContext uniwikiContext, TextService textService)
        {
            _uniwikiContext = uniwikiContext;
            _textService = textService;
        }

        public void SaveEmailConfirmationSecret(EmailConfirmationSecretModel emailConfirmationSecret)
        {
            _uniwikiContext.EmailConfirmationSecrets.Add(emailConfirmationSecret);
        }

        public void ConfirmEmail(EmailConfirmationSecretModel secret)
        {
            var profile = secret.Profile;

            profile.SetAsConfirmed();
        }

        public void InvalidateSecrets(Guid profileId)
        {
            _uniwikiContext.EmailConfirmationSecrets
                .Where(s => s.ProfileId == profileId && s.IsValid)
                .ToList()
                .ForEach(s => s.Invalidate());
        }

        public EmailConfirmationSecretModel? TryGetValidEmailConfirmationSecret(Guid profileId)
        {
           return  _uniwikiContext
                .EmailConfirmationSecrets
                .FirstOrDefault(s => s.ProfileId == profileId && s.IsValid);
        }

        public EmailConfirmationSecretModel AddEmailConfirmationSecret(Guid profileId, Guid secret, DateTime creationTime)
        {
            EmailConfirmationSecretModel emailConfirmationSecretModel = new EmailConfirmationSecretModel(Guid.NewGuid(), profileId, secret, creationTime, true);

            _uniwikiContext.EmailConfirmationSecrets.Add(emailConfirmationSecretModel);

            return emailConfirmationSecretModel;
        }
    }
}

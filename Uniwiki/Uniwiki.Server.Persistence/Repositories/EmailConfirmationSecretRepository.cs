using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class EmailConfirmationSecretRepository : RepositoryBase<EmailConfirmationSecretModel, Guid>, IEmailConfirmationSecretRepository
    {
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.EmailConfirmationSecretNotFound;

        public EmailConfirmationSecretRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.EmailConfirmationSecrets)
        {
            _textService = textService;
        }

        public void SaveEmailConfirmationSecret(EmailConfirmationSecretModel emailConfirmationSecret)
        {
            All.Add(emailConfirmationSecret);
        }

        public void ConfirmEmail(EmailConfirmationSecretModel secret)
        {
            var profile = secret.Profile;
            profile.SetAsConfirmed();
        }

        public void InvalidateSecrets(ProfileModel profile)
        {
            foreach (var emailConfirmationSecretModel in All.Where(s => s.Profile == profile && s.IsValid))
            {
                emailConfirmationSecretModel.Invalidate();
            }
        }

        public EmailConfirmationSecretModel? TryGetValidEmailConfirmationSecret(ProfileModel profile)
        {
            return All.FirstOrDefault(s => s.Profile.Id == profile.Id && s.IsValid);
        }

        public EmailConfirmationSecretModel FindValidById(Guid secret)
        {
            return All.FirstOrDefault(s => s.Secret == secret && s.IsValid) 
                   ?? throw new RequestException(_textService.Error_EmailConfirmationFailed);
        }

        public EmailConfirmationSecretModel AddEmailConfirmationSecret(ProfileModel profile, Guid secret, DateTime creationTime)
        {
            EmailConfirmationSecretModel emailConfirmationSecretModel = new EmailConfirmationSecretModel(Guid.NewGuid(), profile, secret, creationTime, true);

            All.Add(emailConfirmationSecretModel);

            SaveChanges();

            return emailConfirmationSecretModel;
        }
    }
}

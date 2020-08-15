using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface IEmailConfirmationSecretRepository : IRepositoryBase<EmailConfirmationSecretModel, Guid>
    {
        //EmailConfirmationSecretModel GenerateEmailConfirmationSecret(ProfileModel profile, DateTime creationTime);
        void ConfirmEmail(EmailConfirmationSecretModel secret);
        void InvalidateSecrets(ProfileModel profile);
        EmailConfirmationSecretModel? TryGetValidEmailConfirmationSecret(ProfileModel profile);
        EmailConfirmationSecretModel FindValidById(Guid secret);
        //EmailConfirmationSecretModel FindById(Guid secret);
        void SaveEmailConfirmationSecret(EmailConfirmationSecretModel emailConfirmationSecret);
        EmailConfirmationSecretModel AddEmailConfirmationSecret(ProfileModel profile, Guid secret, DateTime creationTime);
        EmailConfirmationSecretModel FindSecret(Guid secret);
    }
}
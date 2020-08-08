using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface IEmailConfirmationSecretRepository : IIdRepository<EmailConfirmationSecretModel, Guid>
    {
        //EmailConfirmationSecretModel GenerateEmailConfirmationSecret(ProfileModel profile, DateTime creationTime);
        void ConfirmEmail(EmailConfirmationSecretModel secret);
        void InvalidateSecrets(ProfileModel profile);
        EmailConfirmationSecretModel? TryGetValidEmailConfirmationSecret(ProfileModel profile);
        EmailConfirmationSecretModel FindValidById(Guid secret);
        EmailConfirmationSecretModel FindById(Guid secret);
        void SaveEmailConfirmationSecret(EmailConfirmationSecretModel emailConfirmationSecret);
    }
}
using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Models.Authentication;

namespace Uniwiki.Server.Persistence.Repositories.Authentication
{
    public interface IEmailConfirmationSecretRepository
    {
        EmailConfirmationSecretModel GenerateEmailConfirmationSecret(ProfileModel profile, DateTime creationTime);
        void ConfirmEmail(EmailConfirmationSecretModel secret);
        void InvalidateSecrets(ProfileModel profile);
        EmailConfirmationSecretModel? TryGetValidEmailConfirmationSecret(ProfileModel profile);
        EmailConfirmationSecretModel FindValidById(Guid secret);
        EmailConfirmationSecretModel FindById(Guid secret);
    }
}
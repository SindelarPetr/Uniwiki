using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Models.Authentication;

namespace Uniwiki.Server.Persistence.Repositories.Authentication
{
    public interface INewPasswordSecretRepository
    {
        NewPasswordSecretModel GenerateNewPasswordSecret(ProfileModel profile, DateTime creationTime, DateTime expirationTime);
        ProfileModel GetProfileForNewPasswordSecret(Guid secret);
        NewPasswordSecretModel TryGetSecretForProfile(ProfileModel profile);
        void InvalidateSecrets(ProfileModel profile);
        NewPasswordSecretModel GetValidSecretById(Guid secret);
    }
}
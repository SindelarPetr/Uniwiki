using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface INewPasswordSecretRepository : IIdRepository<NewPasswordSecretModel, Guid>
    {
        //NewPasswordSecretModel GenerateNewPasswordSecret(ProfileModel profile, DateTime creationTime, DateTime expirationTime);
        ProfileModel GetProfileForNewPasswordSecret(Guid secret);
        NewPasswordSecretModel TryGetSecretForProfile(ProfileModel profile);
        void InvalidateSecrets(ProfileModel profile);
        NewPasswordSecretModel GetValidSecretById(Guid secret);
    }
}
using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface INewPasswordSecretRepository : IRepositoryBase<NewPasswordSecretModel, Guid>
    {
        ProfileModel GetProfileForNewPasswordSecret(Guid secret);
        NewPasswordSecretModel? TryGetSecretForProfile(ProfileModel profile);
        void InvalidateSecrets(ProfileModel profile);
        NewPasswordSecretModel GetValidSecretById(Guid secret);
        NewPasswordSecretModel AddNewPasswordSecret(ProfileModel profile, Guid secret, DateTime creationTime, DateTime expirationTime);
    }
}
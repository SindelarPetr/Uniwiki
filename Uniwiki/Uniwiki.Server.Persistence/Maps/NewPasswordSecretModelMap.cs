using System;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class NewPasswordSecretModelMap : ModelMapBase<NewPasswordSecretModel, Guid>
    {
        public NewPasswordSecretModelMap() : base("NewPasswordSecrets")
        {
        }
    }
}
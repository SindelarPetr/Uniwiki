using System;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class EmailConfirmationSecretModelMap : ModelMapBase<EmailConfirmationSecretModel, Guid>
    {
        public EmailConfirmationSecretModelMap() : base("EmailConfirmationSecrets")
        {
        }
    }
}
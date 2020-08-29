using System;
using System.Runtime.Serialization;
using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class RegisterResponseDto : ResponseBase
    {
        public string UserEmail { get; }

        public Guid UserId { get;  }

        public RegisterResponseDto(string userEmail, Guid userId)
        {
            UserEmail = userEmail;
            UserId = userId;
        }
    }
}

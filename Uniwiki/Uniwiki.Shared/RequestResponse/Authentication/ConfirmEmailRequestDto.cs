using System;
using Shared.RequestResponse;

namespace Uniwiki.Shared.RequestResponse.Authentication
{
    public class ConfirmEmailRequestDto : RequestBase<ConfirmEmailResponseDto>
    {
        public Guid Secret { get; set; }

        public ConfirmEmailRequestDto(Guid secret)
        {
            Secret = secret;
        }
    }
}
using Shared.Exceptions;
using Uniwiki.Server.Application.Services.Abstractions;

namespace Uniwiki.Server.Application.Services
{
    internal class InputValidationService : IInputValidationService
    {
        private readonly TextService _textService;

        public InputValidationService(TextService textService)
        {
            _textService = textService;
        }

        public void ValidateEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if(addr.Address != email)
                {
                    throw new RequestException(_textService.Error_EmailHasWrongFormat);
                }
            }
            catch
            {
                throw new RequestException(_textService.Error_EmailHasWrongFormat);
            }
        }
    }
}

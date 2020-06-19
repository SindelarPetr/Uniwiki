using System;
using Shared.RequestResponse;
using Uniwiki.Shared.Services;

namespace Server.Appliaction.Services
{
    internal class TextService
    {
        private readonly TextServiceBase _textServiceBase;

        public TextService(TextServiceBase textServiceBase)
        {
            _textServiceBase = textServiceBase;
        }


        public string RequestWasNotSuccessfullyValidated =>
            _textServiceBase.GetTranslation("Požadavek neprošel validací.",
                "The request was not successfully validated.");

        public string Error_YouAreNotAuthorizedForThisRequest => _textServiceBase.GetTranslation("Pro tento požadavek nemáte dostatečná oprávnění.",
            "You are not authorized for this request.");

        public string Error_YouNeedToLogInForThis =>
            _textServiceBase.GetTranslation("Pro tuto akci je třeba být přihlášen.",
                "You need to log in for this action");

        public string Error_FailedToRecogniseRequest(IRequest actualRequest, Type expectedRequestType) =>
            _textServiceBase.GetTranslation(
                $"Nepodařilo se přeložit požadavek typu {actualRequest.GetType().FullName} na {expectedRequestType.FullName}", 
                $"Failed to cast request {actualRequest.GetType().FullName} to {expectedRequestType.FullName}.");

        public string Error_NoServerAction(IRequest request)
            => _textServiceBase.GetTranslation($"Server neumí zpracovat požadavek typu {request.GetType().FullName}",
                $"The server does not know how to handle the request {request.GetType().FullName}");
    }
}

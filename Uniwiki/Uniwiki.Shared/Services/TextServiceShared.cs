using Shared;
using System;
using System.Text.Encodings.Web;
using System.Web;
using Uniwiki.Shared.RequestResponse.Authentication;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Shared.Services
{
    internal class TextServiceShared
    {
        private readonly ILanguageService _languageService;

        public TextServiceShared(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public string Validation_FillNameAndSurname => _languageService.GetTranslation("Vyplňte jméno i příjmení.", "Fill in the name and surname.");

        public string Validation_TypeYourPassword => _languageService.GetTranslation("Zadejte heslo.", "Type your password.");
        public string Validation_TypeYourOldPassword => _languageService.GetTranslation("Zadejte Vaše staré heslo.", "Type your old password.");
        public string Validation_TypeYourNewPassword => _languageService.GetTranslation("Zadejte Vaše nové heslo.", "Type your new password.");
        public string Validation_TypeYourEmail => _languageService.GetTranslation("Zadejte email.", "Type your email.");
        public string Validation_TypeValidEmailAddress => _languageService.GetTranslation("Zadejte platnou emailovou adresu.", "Type a valid email address.");
        public string Validation_TypePasswordMatchingRequirements => _languageService.GetTranslation("Heslo musí být alespoň 1 znak dlouhé.", "The password must have at least one letter.");
        public string Validation_OldAndNewPasswordsCantMatch => _languageService.GetTranslation("Staré a nové heslo se nesmí shodovat.", "The old and the new password can not match.");
        public string Validation_PasswordIsNotRepeatedCorrectly => _languageService.GetTranslation("Heslo není zopakováno správně.", "The password is not repeated correctly.");
        public string Validation_NameLengthMustBeAtleastLong => _languageService.GetTranslation("Jméno musí mít alespoň 2 znaky.", "Your name must have at least 2 letters.");

        internal string Validation_YouNeedToAgreeToTermsOfUse => _languageService.GetTranslation("Musíte souhlasit s podmínkami užití.", "You have to agree with the terms of use.");
        public string Validation_SurnameLengthMustBeAtleastLong => _languageService.GetTranslation("Příjmení musí mít alespoň 2 znaky", "Your surename must have at least 2 letters.");
        public string Validation_NonEmpty => _languageService.GetTranslation("Nesmí být prázdné", "Cannot be empty");
        public string Validation_InvalidValue => _languageService.GetTranslation("Neplatná hodnota", "Invalid value");

        public string Validation_MinLength(int minimumLength)
        {
            return minimumLength <= 1
                ? Validation_NonEmpty
                : _languageService.GetTranslation($"Minimální délka je {minimumLength}.", $"Minimum length is {minimumLength}.");
        }

        public string Validation_MaxLength(int maximumLength)
        {
            return _languageService.GetTranslation($"Maximální délka je {maximumLength}.", $"Maximum length is {maximumLength}.");
        }

        public string Validation_YouMustSelectFaculty =>
            _languageService.GetTranslation("Vyberte fakultu, do které patří ten předmět.", "You must select a faculty to which the course belongs.");

        public string Validation_FileNameContainsNonValidCharacters => _languageService.GetTranslation("Název obsahuje nepovolené znaky.", "The name contains not allowed letters.");
    }
}

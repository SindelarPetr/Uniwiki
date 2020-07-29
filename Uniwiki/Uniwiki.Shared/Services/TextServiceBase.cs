using Shared;
using System;
using System.Text.Encodings.Web;
using System.Web;
using Uniwiki.Shared.RequestResponse.Authentication;

namespace Uniwiki.Shared.Services
{
    public abstract class TextServiceBase
    {
        public Language Language { get; private set; }

        protected string Sanitize(string text) => HtmlEncoder.Default.Encode(text);
        public TextServiceBase()
        {
            Language = Constants.DefaultLanguage;
        }

        public void SetLanguage(Language language) => Language = language;

        public string GetTranslation(in string czechText, in string englishText) => Language == Language.Czech ? czechText : englishText;

        public string Validation_TypeYourPassword => GetTranslation("Zadejte heslo.", "Type your password.");
        public string Validation_TypeYourOldPassword => GetTranslation("Zadejte Vaše staré heslo.", "Type your old password.");
        public string Validation_TypeYourNewPassword => GetTranslation("Zadejte Vaše nové heslo.", "Type your new password.");
        public string Validation_TypeYourEmail => GetTranslation("Zadejte email.", "Type your email.");
        public string Validation_TypeValidEmailAddress => GetTranslation("Zadejte platnou emailovou adresu.", "Type a valid email address.");
        public string Validation_TypePasswordMatchingRequirements => GetTranslation("Heslo musí být alespoň 1 znak dlouhé.", "The password must have at least one letter.");
        public string Validation_OldAndNewPasswordsCantMatch => GetTranslation("Staré a nové heslo se nesmí shodovat.", "The old and the new password can not match.");
        public string Validation_PasswordIsNotRepeatedCorrectly => GetTranslation("Heslo není zopakováno správně.", "The password is not repeated correctly.");
        public string Validation_NameLengthMustBeAtleastLong => GetTranslation("Jméno musí mít alespoň 2 znaky.", "Your name must have at least 2 letters.");

        internal string Validation_YouNeedToAgreeToTermsOfUse => GetTranslation("Musíte souhlasit s podmínkami užití.", "You have to agree with the terms of use.");
        public string Validation_SurnameLengthMustBeAtleastLong => GetTranslation("Příjmení musí mít alespoň 2 znaky", "Your surename must have at least 2 letters.");
        public string Validation_NonEmpty => GetTranslation("Nesmí být prázdné", "Cannot be empty");
        public string Validation_InvalidValue => GetTranslation("Neplatná hodnota", "Invalid value");

        public string Validation_MinLength(int minimumLength)
        {
            return minimumLength <= 1
                ? Validation_NonEmpty
                : GetTranslation($"Minimální délka je {minimumLength}.", $"Minimum length is {minimumLength}.");
        }

        public string Validation_MaxLength(int maximumLength)
        {
            return GetTranslation($"Maximální délka je {maximumLength}.", $"Maximum length is {maximumLength}.");
        }

        public string Validation_YouMustSelectFaculty =>
            GetTranslation("Vyberte fakultu, do které patří ten předmět.", "You must select a faculty to which the course belongs.");

        public string Validation_FileNameContainsNonValidCharacters => GetTranslation("Název obsahuje nepovolené znaky.", "The name contains not allowed letters.");
    }
}

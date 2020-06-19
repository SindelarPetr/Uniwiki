namespace Uniwiki.Server.Application.Services.Abstractions
{
    internal interface IInputValidationService
    {
        void ValidateEmail(string email);
    }
}
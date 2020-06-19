namespace Uniwiki.Server.Application.Services.Abstractions
{
    internal interface IEmailTemplateService
    {
        string GetRestorePasswordText(string restoreLink);
        string GetVerifyEmailText(string confirmationLink);
    }
}
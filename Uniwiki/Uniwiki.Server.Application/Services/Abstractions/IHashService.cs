namespace Uniwiki.Server.Application.Services
{
    internal interface IHashService
    {
        (string hashedPassword, byte[] salt) HashPassword(string password);
        string HashPassword(string password, byte[] salt);
    }
}
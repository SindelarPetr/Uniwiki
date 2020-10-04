namespace Uniwiki.Server.Application.Services.Abstractions
{
    public interface IHashService
    {
        (string hashedPassword, byte[] salt) HashPassword(string password);
        string HashPassword(string password, byte[] salt);
    }
}
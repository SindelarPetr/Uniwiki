namespace Shared.Services.Abstractions
{
    public interface IFileHelperService
    {
        (string fileName, string extension) GetFileNameAndExtension(string fullFileName);
    }
}
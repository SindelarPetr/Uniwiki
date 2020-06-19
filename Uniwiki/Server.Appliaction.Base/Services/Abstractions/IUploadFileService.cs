using Microsoft.AspNetCore.Http;

namespace Server.Appliaction.Services.Abstractions
{
    public interface IUploadFileService
    {
        string PostFilesDirectoryPath { get; }
        string EmailTemplatesDirectoryPath { get; }

        void RegisterFile(IFormFile file);

        IFormFile GetFile();
    }
}

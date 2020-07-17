using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Server.Appliaction.Services.Abstractions;

namespace Uniwiki.Server.Host.Services
{
    class UploadFileService : IUploadFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IFormFile _file;

        public UploadFileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        private string basePath => _webHostEnvironment.ContentRootPath;

        public string PostFilesDirectoryPath => Path.Combine(basePath, "uploads");

        public string EmailTemplatesDirectoryPath => Path.Combine(basePath, "EmailTemplates");

        public void RegisterFile(IFormFile file)
        {
            _file = file;
        }

        public IFormFile GetFile()
        {
            return _file;
        }
    }
}
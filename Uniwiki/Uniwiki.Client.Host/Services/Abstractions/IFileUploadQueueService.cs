using System;
using System.Threading.Tasks;
using Uniwiki.Client.Host.Components.FileUploader;

namespace Uniwiki.Client.Host.Services.Abstractions
{
    internal interface IFileUploadQueueService
    {
        Task Upload(UploadFile file, Guid courseId);
    }
}
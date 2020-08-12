using Newtonsoft.Json;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uniwiki.Client.Host.Components.FileUploader;
using Uniwiki.Client.Host.Services.Abstractions;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Client.Host.Services
{
    internal class FileUploadQueueService : IFileUploadQueueService
    {
        private readonly ILoginService _loginService;
        private readonly ILanguageService _languageService;
        Queue<Func<Task<UploadFile>>> _queue;
        int uploading = 0;

        public FileUploadQueueService(ILoginService loginService, ILanguageService languageService)
        {
            _queue = new Queue<Func<Task<UploadFile>>>();
            _loginService = loginService;
            _languageService = languageService;
        }

        public Task Upload(UploadFile file, Guid courseId)
        {
            // Add the file to the queue
            _queue.Enqueue(() => UploadFile(file, courseId));

            return TryExecuteQueue();
        }

        private async Task TryExecuteQueue()
        {
            // Find out if there is an upload spot free
            if (uploading != 0)
            {
                return;
            }

            // Get the uploading token
            uploading++;

            // Execute all elements from the queue one by one
            while (_queue.Any())
            {
                var uploadFunction = _queue.Dequeue();
                var file = await uploadFunction();

                // Wait for the file to finish uploading
                while (!file.Finished)
                {
                    Console.WriteLine($"Waiting for the file '{ file.Name }' to finish uploading.");
                    await Task.Delay(TimeSpan.FromMilliseconds(600));
                }
            }

            // Release the token
            uploading--;
        }



        private async Task<UploadFile> UploadFile(UploadFile uploadFile, Guid courseId)
        {
            try
            {
                var cId = courseId;
                var request = new UploadPostFileRequestDto(uploadFile.Name, cId);
                var requestSerialized = JsonConvert.SerializeObject(request);
                var dataForServer = new DataForServer(requestSerialized, request.GetType(), _loginService.LoginToken?.PrimaryTokenId, _languageService.Language, ClientConstants.AppVersionString);
                await uploadFile.Upload(JsonConvert.SerializeObject(dataForServer));
                return uploadFile;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

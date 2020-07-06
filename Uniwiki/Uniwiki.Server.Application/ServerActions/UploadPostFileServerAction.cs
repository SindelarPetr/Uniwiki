using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Server.Appliaction.ServerActions;
using Server.Appliaction.Services.Abstractions;
using Shared.Exceptions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Repositories.Authentication;
using Uniwiki.Shared.RequestResponse;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class UploadPostFileServerAction : ServerActionBase<UploadPostFileRequestDto, UploadPostFileResponseDto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPostFileRepository _postFileRepository;
        private readonly ITimeService _timeService;
        private readonly IUploadFileService _uploadFileService;
        private readonly ILogger<UploadPostFileServerAction> _logger;

        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public UploadPostFileServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository, IPostFileRepository postFileRepository, ITimeService timeService, IUploadFileService uploadFileService, ILogger<UploadPostFileServerAction> logger) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postFileRepository = postFileRepository;
            _timeService = timeService;
            _uploadFileService = uploadFileService;
            _logger = logger;
        }

        protected override async Task<UploadPostFileResponseDto> ExecuteAsync(UploadPostFileRequestDto request, RequestContext context)
        {
            // Get the user
            var profile = _profileRepository.FindById(context.User.Id);

            // Get the file
            var file = _uploadFileService.GetFile();

            // Create id (which is a name as well) for the file
            Guid id = Guid.NewGuid();

            // Create path for saving the file
            var dirPath = _uploadFileService.PostFilesDirectoryPath;

            // Create uploads directory, if it does not exist
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            // Path to file
            var path = Path.Combine(dirPath, id.ToString());

            // Get the original name of the file
            var originalName = file.FileName;

            // Get the creation time of the file
            var creationTime = _timeService.Now;

            // Log information about the file
            _logger.LogInformation("Writing the file record to the DB: FileId: '{FileId}', FileName: '{FileName}', Size: {Size}", id, originalName, file.Length);

            // Create a new file record in the DB
            var postFileModel = _postFileRepository.CreatePostFile(id, path, originalName, profile, request.CourseId, creationTime, file.Length);

            // Log information about the file
            _logger.LogInformation("Copying the file to the file system: FileId: '{FileId}'", id);

            try
            {
                // Save the file
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception exception)
            {
                // Log the exception
                _logger.LogWarning(0, exception, "Could not upload the file: FileId: '{FileId}', FileName: '{FileName}'", id, originalName);
                throw new RequestException($"Was not able to upload the file {originalName}, the storage on the server is probably full. We recommend you to contact the support.");
            }

            // Set the file as saved
            _postFileRepository.FileSaved(postFileModel);

            // Create DTO
            var postFileDto = postFileModel.ToDto();

            return new UploadPostFileResponseDto(postFileDto);
        }
    }
}

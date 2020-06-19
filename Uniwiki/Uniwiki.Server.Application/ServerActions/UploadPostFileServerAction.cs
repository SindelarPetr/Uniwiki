using System;
using System.IO;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Server.Appliaction.Services.Abstractions;
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
        protected override AuthenticationLevel AuthenticationLevel => Persistence.AuthenticationLevel.PrimaryToken;

        public UploadPostFileServerAction(IServiceProvider serviceProvider, IProfileRepository profileRepository, IPostFileRepository postFileRepository, ITimeService timeService, IUploadFileService uploadFileService):base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postFileRepository = postFileRepository;
            _timeService = timeService;
            _uploadFileService = uploadFileService;
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

            // Create a new file record in the DB
            var postFileModel = _postFileRepository.CreatePostFile(id, path, originalName, profile, request.CourseId, creationTime, file.Length);

            // Save the file
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Set the file as saved
            _postFileRepository.FileSaved(postFileModel);

            // Create DTO
            var postFileDto = postFileModel.ToDto();

            return new UploadPostFileResponseDto(postFileDto);
        }
    }
}

﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Server.Appliaction.ServerActions;
using Server.Appliaction.Services.Abstractions;
using Shared.Exceptions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Extensions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class UploadPostFileServerAction : ServerActionBase<UploadPostFileRequestDto, UploadPostFileResponseDto>
    {
        private readonly ProfileRepository _profileRepository;
        private readonly PostFileRepository _postFileRepository;
        private readonly ITimeService _timeService;
        private readonly IUploadFileService _uploadFileService;
        private readonly ILogger<UploadPostFileServerAction> _logger;
        private readonly IFileHelperService _fileHelperService;
        private readonly CourseRepository _courseRepository;
        private readonly TextService _textService;
        private readonly UniwikiContext _uniwikiContext;

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.PrimaryToken;

        public UploadPostFileServerAction(IServiceProvider serviceProvider, ProfileRepository profileRepository, PostFileRepository postFileRepository, ITimeService timeService, IUploadFileService uploadFileService, ILogger<UploadPostFileServerAction> logger, IFileHelperService fileHelperService, CourseRepository courseRepository, TextService textService, UniwikiContext uniwikiContext) : base(serviceProvider)
        {
            _profileRepository = profileRepository;
            _postFileRepository = postFileRepository;
            _timeService = timeService;
            _uploadFileService = uploadFileService;
            _logger = logger;
            _fileHelperService = fileHelperService;
            _courseRepository = courseRepository;
            _textService = textService;
            _uniwikiContext = uniwikiContext;
        }

        protected override async Task<UploadPostFileResponseDto> ExecuteAsync(UploadPostFileRequestDto request, RequestContext context)
        {
            // Get the file
            var file = _uploadFileService.GetFile();

            // Create id for the file (which we use as the name for it as well)
            var id = Guid.NewGuid();

            // Create path for saving the file
            var dirPath = _uploadFileService.PostFilesDirectoryPath;

            // Create uploads directory, if it does not exist
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            // Path to file
            var path = Path.Combine(dirPath, id.ToString());

            // Get the original name of the file
            var originalName = file.FileName;

            // Get the file name and extension
            var (fileName, extension) = _fileHelperService.GetFileNameAndExtension(originalName);

            // Get the creation time of the file
            var creationTime = _timeService.Now;

            // Log information about the file
            _logger.LogInformation("Writing the file record to the DB: FileId: '{FileId}', FileName: '{FileName}', Size: {Size}", id, originalName, file.Length);

            // Create a new file record in the DB
            var postFileId = _postFileRepository.AddPostFile(id, path, fileName, extension, false, context.UserId!.Value, request.CourseId, creationTime, file.Length);

            // Save the changes to make sure there is a record about the file (and we need to load it a few lines later)
            _uniwikiContext.SaveChanges();

            // Log information about the file
            _logger.LogInformation("Copying the file to the file system: FileId: '{FileId}'", id);

            try
            {
                // Save the file
                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
            }
            catch (Exception exception)
            {
                // Log the exception
                _logger.LogWarning(0, exception, "Could not upload the file: FileId: '{FileId}', FileName: '{FileName}'", id, originalName);
                throw new RequestException(_textService.UploadPostFile(fileName));
            }

            // Set the file as saved
            _postFileRepository.FileSaved(_uniwikiContext.PostFiles.First(f => f.Id == postFileId));

            var postFileDto = _uniwikiContext.PostFiles.AsNoTracking().Where(f => f.Id == postFileId).ToPostFileDto().First();

            return new UploadPostFileResponseDto(postFileDto);
        }
    }
}

﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Server.Appliaction.ServerActions;
using Server.Appliaction.Services.Abstractions;
using Shared.Exceptions;
using Shared.Services.Abstractions;
using Uniwiki.Server.Application.Services;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Server.Application.ServerActions
{

    internal class GetPostFileServerAction : ServerActionBase<GetPostFileRequest, GetPostFileResponse>
    {
        private readonly PostFileRepository _postFileRepository;
        private readonly IUploadFileService _uploadFileService;
        private readonly PostFileDownloadRepository _postFileDownloadRepository;
        private readonly ITimeService _timeService;
        private readonly TextService _textService;

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.SecondaryToken;

        public GetPostFileServerAction(IServiceProvider serviceProvider, PostFileRepository postFileRepository, IUploadFileService uploadFileService, PostFileDownloadRepository postFileDownloadRepository, ITimeService timeService, TextService textService):base(serviceProvider)
        {
            _postFileRepository = postFileRepository;
            _uploadFileService = uploadFileService;
            _postFileDownloadRepository = postFileDownloadRepository;
            _timeService = timeService;
            _textService = textService;
        }

        protected override Task<GetPostFileResponse> ExecuteAsync(GetPostFileRequest request, RequestContext context)
        {
            // Get the last time the user downloaded the file
            var latestDownload = _postFileDownloadRepository.TryGetLatestDownload(context.LoginToken!.Id, request.FileId);

            // Get current time
            var currentTime = _timeService.Now;

            // Check if the download time is longer than required time
            if (latestDownload != null && latestDownload.DownloadTime + Constants.DownloadAgainTime > currentTime)
            {
                throw new RequestException(_textService.Error_WaitBeforeRepeatedDownload);
            }

            // Add it to the DB
            _postFileDownloadRepository.AddPostFileDownload(context.LoginToken!.Id, request.FileId, currentTime);

            // Get path for the file
            var filePath = Path.Combine(_uploadFileService.PostFilesDirectoryPath, request.FileId.ToString());

            // Get original name for the file
            var originalFullName = _postFileRepository.FindById(request.FileId).Select(f => f.OriginalFullName).Single();

            // Open the file stream
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            return Task.FromResult(new GetPostFileResponse(fileStream, originalFullName));
        }
    }
}

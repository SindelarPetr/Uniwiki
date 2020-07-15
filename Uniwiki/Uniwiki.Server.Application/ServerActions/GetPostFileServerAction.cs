using System;
using System.IO;
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
        private readonly IPostFileRepository _postFileRepository;
        private readonly IUploadFileService _uploadFileService;
        private readonly IPostFileDownloadRepository _postFileDownloadRepository;
        private readonly ITimeService _timeService;
        private readonly TextService _textService;

        protected override AuthenticationLevel AuthenticationLevel => AuthenticationLevel.SecondaryToken;

        public GetPostFileServerAction(IServiceProvider serviceProvider, IPostFileRepository postFileRepository, IUploadFileService uploadFileService, IPostFileDownloadRepository postFileDownloadRepository, ITimeService timeService, TextService textService):base(serviceProvider)
        {
            _postFileRepository = postFileRepository;
            _uploadFileService = uploadFileService;
            _postFileDownloadRepository = postFileDownloadRepository;
            _timeService = timeService;
            _textService = textService;
        }

        protected override Task<GetPostFileResponse> ExecuteAsync(GetPostFileRequest request, RequestContext context)
        {
            // Get file from DB
            var file = _postFileRepository.FindById(request.FileId, request.ExpectedName);

            // Get the last time the user downloaded the file
            var latestDownload = _postFileDownloadRepository.TryGetLatestDownload(context.LoginToken, file);

            // Get current time
            var currentTime = _timeService.Now;

            // Check if the download time is longer than required time
            if (latestDownload != null && latestDownload.DownloadTime + Constants.DownloadAgainTime > currentTime)
                throw new RequestException(_textService.Error_WaitBeforeRepeatedDownload);

            // Add info about the file being downloaded
            _postFileDownloadRepository.AddDownload(context.LoginToken, file, currentTime);

            // Get path for the file
            var filePath = Path.Combine(_uploadFileService.PostFilesDirectoryPath, request.FileId.ToString());

            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            return Task.FromResult(new GetPostFileResponse(fileStream, file.OriginalName));
        }
    }
}

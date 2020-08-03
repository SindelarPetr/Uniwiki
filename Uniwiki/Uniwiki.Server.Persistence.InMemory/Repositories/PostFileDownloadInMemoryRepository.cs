using System;
using System.Collections.Generic;
using System.Linq;
using Shared;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Services;
using Uniwiki.Server.Persistence.InMemory.Services;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Models.Authentication;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Server.Persistence.Services;
using Uniwiki.Shared;

namespace Uniwiki.Server.Persistence.InMemory.Repositories
{
    internal class PostFileDownloadInMemoryRepository : IPostFileDownloadRepository
    {
        private readonly DataService _dataService;

        public PostFileDownloadInMemoryRepository(DataService dataService)
        {
            _dataService = dataService;
        }

        public PostFileDownload AddDownload(LoginTokenModel token, PostFileModel fileDownloaded, DateTime downloadTime)
        {
            // Create the download
            var postFileDownload = new PostFileDownload(token, fileDownloaded, downloadTime);

            // Add the download to the DB
            _dataService._postFileDownloads.Add(postFileDownload);

            return postFileDownload;
        }

        public PostFileDownload? TryGetLatestDownload(LoginTokenModel token, PostFileModel fileDownloaded)
        {
            return _dataService.PostFileDownloads.LastOrDefault(d => d.Token == token && d.FileDownloaded == fileDownloaded);
        }
    }
}

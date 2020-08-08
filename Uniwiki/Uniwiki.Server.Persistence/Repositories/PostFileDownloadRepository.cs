using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class PostFileDownloadRepository : IPostFileDownloadRepository
    {
        private readonly UniwikiContext _uniwikiContext;

        public PostFileDownloadRepository(UniwikiContext uniwikiContext)
        {
            _uniwikiContext = uniwikiContext;
        }

        public PostFileDownloadModel AddDownload(LoginTokenModel token, PostFileModel fileDownloaded, DateTime downloadTime)
        {
            // Create the download
            var postFileDownload = new PostFileDownloadModel(token, fileDownloaded, downloadTime);

            // Add the download to the DB
            _uniwikiContext.AllPostFileDownloads.Add(postFileDownload);

            return postFileDownload;
        }

        public PostFileDownloadModel? TryGetLatestDownload(LoginTokenModel token, PostFileModel fileDownloaded)
        {
            return _uniwikiContext.PostFileDownloads.LastOrDefault(d => d.Token == token && d.FileDownloaded == fileDownloaded);
        }
    }
}

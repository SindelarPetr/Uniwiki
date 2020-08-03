using System;
using System.Collections.Generic;
using System.Text;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Models.Authentication;

namespace Uniwiki.Server.Persistence.Repositories
{
    public interface IPostFileDownloadRepository
    {
        PostFileDownloadModel AddDownload(LoginTokenModel token, PostFileModel fileDownloaded, DateTime downloadTime);
        PostFileDownloadModel? TryGetLatestDownload(LoginTokenModel token, PostFileModel fileDownloaded);
    }
}

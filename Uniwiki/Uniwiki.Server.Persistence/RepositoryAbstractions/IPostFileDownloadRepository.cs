﻿using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface IPostFileDownloadRepository : IIdRepository<PostFileDownloadModel, Guid>
    {
        //PostFileDownloadModel AddDownload(LoginTokenModel token, PostFileModel fileDownloaded, DateTime downloadTime);
        PostFileDownloadModel? TryGetLatestDownload(LoginTokenModel token, PostFileModel fileDownloaded);
    }
}
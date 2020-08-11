﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class PostFileDownloadRepository : RepositoryBase<PostFileDownloadModel>, IPostFileDownloadRepository
    {
        private readonly TextService _textService;

        public string NotFoundByIdMessage => _textService.Error_PostFileDownloadNotFound;

        public PostFileDownloadRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.PostFileDownloads)
        {
            _textService = textService;
        }


        public PostFileDownloadModel? TryGetLatestDownload(LoginTokenModel token, PostFileModel fileDownloaded)
        {
            return All.LastOrDefault(d => d.Token == token && d.FileDownloaded == fileDownloaded);
        }
    }
}

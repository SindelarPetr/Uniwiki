using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    public class PostFileDownloadRepository : RepositoryBase<PostFileDownloadModel, Guid> 
    {
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.Error_PostFileDownloadNotFound;

        public PostFileDownloadRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.PostFileDownloads)
        {
            _textService = textService;
        }

        public PostFileDownloadModel? TryGetLatestDownload(Guid loginTokenId, Guid fileDownloadedId) 
            => All.LastOrDefault(d => d.TokenId == loginTokenId && d.FileDownloadedId == fileDownloadedId);

        public PostFileDownloadModel AddPostFileDownload(Guid loginTokenId, Guid fileDownloadedId, DateTime downloadTime)
        {
            var postFileDownload = new PostFileDownloadModel(Guid.NewGuid(), loginTokenId, fileDownloadedId, downloadTime);

            All.Add(postFileDownload);

            return postFileDownload;
        }
    }
}

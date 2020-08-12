using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class PostFileRepository : RepositoryBase<PostFileModel>, IPostFileRepository
    {
        private readonly TextService _textService;

        bool IRemovableModel.IsRemoved { get; set; }

        public string NotFoundByIdMessage => _textService.PostFileNotFound;

        public PostFileRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.PostFiles)
        {
            _textService = textService;
        }

        public void FileSaved(PostFileModel postFileModel)
        {
            postFileModel.FileSaved();
        }

        public IEnumerable<PostFileModel> FindPostFilesAndUpdateNames(IEnumerable<(Guid id, string fileName)> files, ProfileModel profile)
        {
            var fileModels = files.Select(f =>
                (
                    postFile: All.FirstOrDefault(p => p.Id == f.id && p.Profile == profile),
                    fileName: f.fileName
                )
               ).ToArray();

            var notFoundNames = fileModels.Where(p => p.postFile == null).Select(p => p.fileName).ToArray();
            if (notFoundNames.Any())
                throw new RequestException(_textService.Error_FilesNotFound(notFoundNames));

            // Update names of the file models
            foreach (var fileModel in fileModels)
            {
                var newFileName = files.First(f => f.id == fileModel.postFile.Id).fileName;
                fileModel.postFile.SetFileName(newFileName);
            }

            return fileModels.Select(f => f.postFile);
        }

        public PostFileModel AddPostFile(string path, string nameWithoutExtension, string extension, bool isSaved, ProfileModel profile, Guid courseId, DateTime creationTime, long size)
        {
            var postFile = new PostFileModel(Guid.NewGuid(), path, nameWithoutExtension, extension, isSaved, profile, courseId, creationTime, size, false);

            All.Add(postFile);

            return postFile;
        }

        //public PostFileModel FindById(Guid fileId, string fileName)
        //{
        //    return All.FirstOrDefault(f => f.Id == fileId) ??
        //           throw new RequestException(_textService.Error_CouldNotFindFile(fileName));
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class PostFileRepository : IPostFileRepository
    {
        private readonly UniwikiContext _uniwikiContext;
        private readonly TextService _textService;

        public PostFileRepository(UniwikiContext uniwikiContext, TextService textService)
        {
            _uniwikiContext = uniwikiContext;
            _textService = textService;
        }

        public PostFileModel CreatePostFile(Guid id, string path, string fileName, string extension, ProfileModel profile, Guid courseId, DateTime creationTime, long size)
        {
            // Create the new file
            var postFile = new PostFileModel(id, path, fileName, extension, false, profile, courseId, creationTime, size);

            // Save it to DB
            _uniwikiContext.PostFiles.Add(postFile);

            return postFile;
        }

        public void FileSaved(PostFileModel postFileModel)
        {
            postFileModel.FileSaved();
        }

        public IEnumerable<PostFileModel> FindPostFilesAndUpdateNames(IEnumerable<(Guid id, string fileName)> files, ProfileModel profile)
        {
            var fileModels = files.Select(f =>
                (postFile: _uniwikiContext.PostFiles.FirstOrDefault(p => p.Id == f.id && p.Profile == profile), fileName: f.fileName)).ToArray();
            
            var notFoundNames = fileModels.Where(p => p.postFile == null).Select(p => p.fileName).ToArray();
            if(notFoundNames.Any())
                throw new RequestException(_textService.Error_FilesNotFound(notFoundNames));

            // Update names of the file models
            foreach (var fileModel in fileModels)
            {
                var newFileName = files.First(f => f.id == fileModel.postFile.Id).fileName;
                fileModel.postFile.SetFileName(newFileName);
            }

            return fileModels.Select(f => f.postFile);
        }

        public PostFileModel FindById(Guid fileId, string fileName)
        {
            return _uniwikiContext.PostFiles.FirstOrDefault(f => f.Id == fileId) ??
                   throw new RequestException(_textService.Error_CouldNotFindFile(fileName));
        }
    }
}
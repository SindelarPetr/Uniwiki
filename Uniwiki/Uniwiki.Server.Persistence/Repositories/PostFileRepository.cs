using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;

using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    public class PostFileRepository : RepositoryBase<PostFileModel, Guid> 
    {
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.PostFileNotFound;

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
                    f.fileName
                )
               ).ToArray();

            var notFoundNames = fileModels.Where(p => p.postFile == null).Select(p => p.fileName).ToArray();
            if (notFoundNames.Any())
            {
                throw new RequestException(_textService.Error_FilesNotFound(notFoundNames));
            }

            // Update names of the file models
            foreach (var fileModel in fileModels)
            {
                var newFileName = files.First(f => f.id == fileModel.postFile.Id).fileName;
                fileModel.postFile.SetFileNameWithoutExtension(newFileName);
            }

            return fileModels.Select(f => f.postFile);
        }

        public Guid AddPostFile(Guid id, string path, string nameWithoutExtension, string extension, bool isSaved, Guid profileId, Guid courseId, DateTime creationTime, long size)
        {
            var postFile = new PostFileModel(id, path, nameWithoutExtension, extension, isSaved, profileId, courseId, creationTime, size);

            All.Add(postFile);

            return postFile.Id;
        }

        /// <summary>
        /// Finds the post files.
        /// </summary>
        /// <param name="files">The files with their names. The names are there just for the reason of throwing an exception with their name, if some of the files is not found.</param>
        /// <param name="profile">The profile of the user who is adding the files to the DB.</param>
        /// <returns>All the found files.</returns>
        /// <exception cref="RequestException">Thrown when a file is not found.</exception>
        public IEnumerable<PostFileModel> FindPostFiles(IEnumerable<(Guid id, string fileName)> files, Guid profileId)
        {
            // Find the post files according to the defined Ids. Set postFile to null, if not found.
            var fileModels = files.Select(f =>
                (
                    postFile: All.FirstOrDefault(p => p.Id == f.id && p.ProfileId == profileId), 
                    f.fileName
                )
               ).ToArray();

            // Find which postFiles are null.
            var notFoundNames = fileModels.Where(p => p.postFile == null).Select(p => p.fileName).ToArray();

            // Throw the exception for all not found postFiles or return the file models
            return notFoundNames.Any()
                ? throw new RequestException(_textService.Error_FilesNotFound(notFoundNames))
                : fileModels.Select(pf => pf.postFile);
        }

        public void UpdateNamesOfPostFiles(IEnumerable<(PostFileModel postFile, string newFileName)> files)
        {
            foreach (var file in files)
            {
                file.postFile.SetFileNameWithoutExtension(file.newFileName);
            }
        }

        public void PairPostFilesWithPost(PostFileModel[] files, Guid postId)
        {
            foreach (var file in files)
            {
                file.SetPostId(postId);
            }
        }
    }
}
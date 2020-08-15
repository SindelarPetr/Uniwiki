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
    internal class PostFileRepository : RepositoryBase<PostFileModel, Guid>, IPostFileRepository
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

        public PostFileModel AddPostFile(string path, string nameWithoutExtension, string extension, bool isSaved, ProfileModel profile, CourseModel course, DateTime creationTime, long size)
        {
            var postFile = new PostFileModel(Guid.NewGuid(), path, nameWithoutExtension, extension, isSaved, profile, course, creationTime, size, false);

            All.Add(postFile);

            return postFile;
        }

        /// <summary>
        /// Finds the post files.
        /// </summary>
        /// <param name="files">The files with their names. The names are there just for the reason of throwing an exception with their name, if some of the files is not found.</param>
        /// <param name="profile">The profile of the user who is adding the files to the DB.</param>
        /// <returns>All the found files.</returns>
        /// <exception cref="RequestException">Thrown when a file is not found.</exception>
        public IEnumerable<PostFileModel> FindPostFiles(IEnumerable<(Guid id, string fileName)> files, ProfileModel profile)
        {
            // Find the post files according to the defined Ids. Set postFile to null, if not found.
            var fileModels = files.Select(f =>
                (
                    postFile: All.FirstOrDefault(p => p.Id == f.id && p.Profile == profile), 
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

        public IEnumerable<PostFileModel> UpdateNamesOfPostFiles(IEnumerable<(PostFileModel postFile, string newFileName)> files)
        {
            foreach (var file in files)
            {
                file.postFile.SetFileNameWithoutExtension(file.newFileName);
            }

            SaveChanges();

            return files.Select(f => f.postFile);
        }

        public PostModel PairPostFilesWithPost(PostFileModel[] files, PostModel post)
        {
            post.SetPostFiles(files);

            foreach (var file in files)
            {
                file.SetPost(post);
            }

            SaveChanges();

            return post;
        }
    }
}
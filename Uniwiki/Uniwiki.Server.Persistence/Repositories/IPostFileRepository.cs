using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Models;

namespace Uniwiki.Server.Persistence.Repositories
{
    public interface IPostFileRepository
    {
        PostFileModel CreatePostFile(Guid id, string path, string fileName, string extension, ProfileModel profile, Guid courseId, DateTime creationTime, long size);
        void FileSaved(PostFileModel postFileModel);
        PostFileModel FindById(Guid fileId, string fileName);
        IEnumerable<PostFileModel> FindPostFilesAndUpdateNames(IEnumerable<(Guid id, string fileName)> files, ProfileModel profile);
    }
}
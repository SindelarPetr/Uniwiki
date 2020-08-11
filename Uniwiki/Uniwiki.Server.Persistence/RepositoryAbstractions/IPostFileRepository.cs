using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface IPostFileRepository : IIdRepository<PostFileModel, Guid>, IRemovableModel
    {
        //PostFileModel CreatePostFile(Guid id, string path, string fileName, string extension, ProfileModel profile, Guid courseId, DateTime creationTime, long size);
        void FileSaved(PostFileModel postFileModel);
        IEnumerable<PostFileModel> FindPostFilesAndUpdateNames(IEnumerable<(Guid id, string fileName)> files, ProfileModel profile);
    }
}
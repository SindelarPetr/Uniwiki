using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;

namespace Uniwiki.Server.Persistence.RepositoryAbstractions
{
    public interface IPostFileRepository : IRepositoryBase<PostFileModel, Guid>
    {
        //PostFileModel CreatePostFile(Guid id, string path, string fileName, string extension, ProfileModel profile, Guid courseId, DateTime creationTime, long size);
        void FileSaved(PostFileModel postFileModel);

        IEnumerable<PostFileModel> FindPostFiles(IEnumerable<(Guid id, string fileName)> files, ProfileModel profile);
        
        PostFileModel AddPostFile(string path, string nameWithoutExtension, string extension, bool isSaved, ProfileModel profile, Guid courseId, DateTime creationTime, long size);
        
        IEnumerable<PostFileModel> UpdateNamesOfPostFiles(IEnumerable<(PostFileModel postFile, string newFileName)> files);
        
        PostModel PairPostFilesWithPost(PostFileModel[] files, PostModel post);
    }
}
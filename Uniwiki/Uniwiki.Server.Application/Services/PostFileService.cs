using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Extensions;
using Uniwiki.Server.Persistence;
using Uniwiki.Server.Persistence.Repositories;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Server.Application.Services
{
    public class PostFileService
    {
        private readonly PostFileRepository _postFileRepository;
        private readonly UniwikiContext _uniwikiContext;

        public PostFileService(PostFileRepository postFileRepository, UniwikiContext uniwikiContext)
        {
            _postFileRepository = postFileRepository;
            _uniwikiContext = uniwikiContext;
        }

        public void UpdatePostFiles(Guid userId, Guid postId, PostFileDto[] postFileDtos)
        {
            // Get files for the post
            var files = postFileDtos.Select(f => (f.Id, f.NameWithoutExtension)).ToArray();
            
            // Get corresponding files from the DB
            var postFiles = _postFileRepository.FindPostFiles(files, userId).ToArray();

            // Get the new post files
            var newPostFiles = postFiles.Where(f => f.PostId == null).ToArray();

            // Get all files with changed names
            var changedPostFiles = postFiles
                .Select(pf => (PostFile: pf, files.First(f => f.Id == pf.Id).NameWithoutExtension))
                .Where(p => p.NameWithoutExtension != p.PostFile.NameWithoutExtension);

            // Update the names for the postFiles
            _postFileRepository.UpdateNamesOfPostFiles(changedPostFiles);

            // Pair all the files to the new post
            _postFileRepository.PairPostFilesWithPost(newPostFiles, postId);

            // Get all the existing files for the post
            var existingFiles = _uniwikiContext.PostFiles.Where(f => f.PostId == postId).ToArray();

            // Find the removed files
            var removedFiles = existingFiles.Where(f => postFiles.All(nf => nf.Id != f.Id)).ToArray();

            // Remove the files
            removedFiles.ForEach(f => f.PostId = null);

            _uniwikiContext.SaveChanges();
        }
    }
}

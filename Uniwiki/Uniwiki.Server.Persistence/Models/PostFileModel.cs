using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostFileModel : RemovableModelBase<Guid>
    {
        public string Path { get; protected set; } = null!;
        public string OriginalFullName => $"{NameWithoutExtension}{Extension}";
        public string NameWithoutExtension { get; protected set; } = null!;
        public string Extension { get; protected set; } = null!;
        public bool IsSaved { get; protected set; }
        public Guid ProfileId { get; protected set; }
        public ProfileModel Profile { get; protected set; } = null!;
        public Guid CourseId { get; protected set; }
        public CourseModel Course { get; protected set; } = null!;
        public DateTime CreationTime { get; protected set; }
        public long Size { get; protected set; }
        public Guid? PostId { get; protected set; } // At the beginning its uninitialized! Because files are uploaded before a post is created
        public PostModel? Post { get; protected set; }

        internal PostFileModel(Guid id, string path, string nameWithoutExtension, string extension, bool isSaved, Guid profileId, Guid courseId, DateTime creationTime, long size, bool isRemoved)
            : base(isRemoved, id)
        {
            Path = path;
            NameWithoutExtension = nameWithoutExtension;
            Extension = extension;
            IsSaved = isSaved;
            ProfileId = profileId;
            CourseId = courseId;
            CreationTime = creationTime;
            Size = size;
        }

        protected PostFileModel(Guid? postId)
        {
            PostId = postId;
        }

        private static FileType GetFileTypeFromExtension(string extension)
        {
            switch (extension)
            {
                case ".doc":
                case ".docx":
                    return FileType.Word;
                case ".jpg":
                case ".png":
                case ".jepg":
                    return FileType.Image;
                case ".pdf":
                    return FileType.Pdf;
                case ".zip":
                case ".rar":
                case ".tar":
                case ".7zip":
                    return FileType.Archive;
                default: return FileType.Other;
            }
        }
        

        internal void FileSaved()
        {
            IsSaved = true;
        }

        internal void SetFileNameWithoutExtension(string newFileNameWithoutExtension)
        {
            NameWithoutExtension = newFileNameWithoutExtension;
        }

        internal void SetPost(PostModel post)
        {
            Post = post;
        }
    }
}
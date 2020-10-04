using System;
using System.ComponentModel.DataAnnotations;
using Uniwiki.Server.Persistence.Models.Base;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Shared;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostFileModel : ModelBase<Guid>
    {

        [MaxLength(300)]
        public string Path { get; set; } = null!;
        [MaxLength(Constants.Validations.FileNameMaxLength + 1 + Constants.Validations.FileExtensionMaxLength)]
        public string OriginalFullName => $"{NameWithoutExtension}{Extension}";
        [MaxLength(Constants.Validations.FileNameMaxLength)]
        public string NameWithoutExtension { get; set; } = null!;
        [MaxLength(Constants.Validations.FileExtensionMaxLength)]
        public string Extension { get; set; } = null!;
        public bool IsSaved { get; set; }
        public Guid ProfileId { get; set; }
        public ProfileModel Profile { get; set; } = null!;
        public Guid CourseId { get; set; }
        public CourseModel Course { get; set; } = null!;
        public DateTime CreationTime { get; set; }
        public long Size { get; set; }
        public Guid? PostId { get; set; } // At the beginning its uninitialized! Because files are uploaded before a post is created
        public PostModel? Post { get; set; }

        public PostFileModel(Guid id, string path, string nameWithoutExtension, string extension, bool isSaved, Guid profileId, Guid courseId, DateTime creationTime, long size)
            : base(id)
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

        internal void SetPostId(Guid postId)
        {
            PostId = postId;
        }
    }
}
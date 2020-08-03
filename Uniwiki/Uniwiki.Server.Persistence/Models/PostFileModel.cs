using System;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostFileModel
    {
        public Guid Id { get; protected set; }
        public string Path { get; protected set; }
        public string OriginalFullName => $"{NameWithoutExtension}{Extension}";
        public string NameWithoutExtension { get; protected set; }
        public string Extension { get; protected set; }
        public bool IsSaved { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public Guid CourseId { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        public long Size { get; protected set; }

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

        internal PostFileModel()
        {

        }

        internal PostFileModel(Guid id, string path, string nameWithoutExtension, string extension, bool isSaved, ProfileModel profile, Guid courseId, DateTime creationTime, long size)
        {
            Id = id;
            Path = path;
            NameWithoutExtension = nameWithoutExtension;
            Extension = extension;
            IsSaved = isSaved;
            Profile = profile;
            CourseId = courseId;
            CreationTime = creationTime;
            Size = size;
        }

        internal void FileSaved()
        {
            IsSaved = true;
        }

        internal void SetFileName(string newFileName)
        {
            NameWithoutExtension = newFileName;
        }
    }
}
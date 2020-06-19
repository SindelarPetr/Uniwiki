using System;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostFileModel
    {
        public Guid Id { get; }
        public string Path { get; }
        public string OriginalName { get; private set; }
        public bool IsSaved { get; private set; }
        public ProfileModel Profile { get; }
        public Guid CourseId { get; }
        public DateTime CreationTime { get; }
        public long Size { get; }

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

        public PostFileModel(Guid id, string path, string originalName, bool isSaved, ProfileModel profile, Guid courseId, DateTime creationTime, long size)
        {
            Id = id;
            Path = path;
            OriginalName = originalName;
            IsSaved = isSaved;
            Profile = profile;
            CourseId = courseId;
            CreationTime = creationTime;
            Size = size;
        }

        public void FileSaved()
        {
            IsSaved = true;
        }

        public void SetFileName(string newFileName)
        {
            OriginalName = newFileName;
        }
    }
}
﻿using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostFileModel : IIdModel<Guid>, IRemovableModel
    {
        public string Path { get; protected set; }
        public string OriginalFullName => $"{NameWithoutExtension}{Extension}";
        public string NameWithoutExtension { get; protected set; }
        public string Extension { get; protected set; }
        public bool IsSaved { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public Guid CourseId { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        public long Size { get; protected set; }
        bool IRemovableModel.IsRemoved { get; set; }

        public Guid Id { get; protected set; }

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


        public PostFileModel(Guid id, string path, string nameWithoutExtension, string extension, bool isSaved, ProfileModel profile, Guid courseId, DateTime creationTime, long size, bool isRemoved)
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
            ((IRemovableModel)this).IsRemoved = isRemoved;
        }

        protected PostFileModel()
        {

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
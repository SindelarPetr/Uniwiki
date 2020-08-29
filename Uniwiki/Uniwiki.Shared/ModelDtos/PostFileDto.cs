using System;

namespace Uniwiki.Shared.ModelDtos
{
    public class PostFileDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Gets the full name of the original file with an extension
        /// </summary>
        public string OriginalFullName => $"{NameWithoutExtension}{Extension}";
        public string Extension { get; set; }
        public string NameWithoutExtension { get; set; }
        public long Size { get; set; }

        public PostFileDto(Guid id, string nameWithoutExtension, string extension, long size)
        {
            Id = id;
            NameWithoutExtension = nameWithoutExtension;
            Extension = extension;
            Size = size;
        }
    }
}
using System;

namespace Uniwiki.Shared.ModelDtos
{
    public class PostFileDto
    {
        public Guid Id { get; set; }
        public string OriginalName { get; set; }
        public bool IsSaved { get; set; }
        public long Size { get; set; }

        public PostFileDto(Guid id, string originalName, bool isSaved, long size)
        {
            Id = id;
            OriginalName = originalName;
            IsSaved = isSaved;
            Size = size;
        }
    }
}
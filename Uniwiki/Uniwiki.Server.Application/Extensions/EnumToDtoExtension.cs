using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Server.Application.Extensions
{
    public static class EnumToDtoExtension
    {
        public static FileTypeDto ToDto(this FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Image:
                    return FileTypeDto.Image;
                case FileType.Pdf:
                    return FileTypeDto.Pdf;
                case FileType.Archive:
                    return FileTypeDto.Archive;
                case FileType.Word:
                    return FileTypeDto.Word;
                case FileType.Other:
                    return FileTypeDto.Other;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileType), fileType, null);
            }
        }
    }
}

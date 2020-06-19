using System;
using Shared.Dtos;

namespace Shared.Extensions
{
    public static class ToDtoExtensions
    {
        public static VersionDto ToDto(this Version version) => new VersionDto(version.ToString());
    }
}

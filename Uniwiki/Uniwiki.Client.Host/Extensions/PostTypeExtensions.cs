using Uniwiki.Client.Host.Services;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Client.Host.Extensions
{
    internal static class PostTypeExtensions
    {
        public static string ToStringSingular(this string? postType, TextService textService) => postType == null ? textService.PostType_Unknown_Singular : postType;

        public static string ToStringPlural(this string? postType, TextService textService) 
            => postType == null ? textService.PostType_Unknown_Plural : postType;
    }
}
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class FilterPostTypeDto
    {
        public string? PostType { get; set; }
        public int Count { get; set; }

        public FilterPostTypeDto(string? postType, int count)
        {
            PostType = postType;
            Count = count;
        }
    }
}
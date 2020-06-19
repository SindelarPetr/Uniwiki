using System;
using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class AddPostRequestDto : RequestBase<AddPostResponseDto>
    {
        public string Text { get; set; }
        public string? PostType { get; set; }
        public Guid CourseId { get; set; }
        public PostFileDto[] PostFiles { get; set; }

        public AddPostRequestDto(string text, string? postType, Guid courseId, PostFileDto[] postFiles)
        {
            Text = text;
            PostType = postType;
            CourseId = courseId;
            PostFiles = postFiles;
        }
    }
}
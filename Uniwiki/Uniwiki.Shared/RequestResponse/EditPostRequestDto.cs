using System;
using System.Collections.Generic;
using Shared.RequestResponse;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class EditPostRequestDto : RequestBase<EditPostResponseDto>
    {
        public Guid PostId { get; set; }
        public string Text { get; set; }
        public string? PostType { get; set; }
        public List<PostFileDto> PostFiles { get; set; }

        public EditPostRequestDto(Guid postId, string text, string? postType, PostFileDto[] postFiles)
        {
            PostId = postId;
            Text = text;
            PostType = postType;
            PostFiles = new List<PostFileDto>(postFiles);
        }
    }
}

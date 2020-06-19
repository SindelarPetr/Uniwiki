using System;
using System.Collections.Generic;
using System.Linq;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Shared.ModelDtos
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public ProfileDto Author { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public int NumberOfFiles { get; set; }
        public string? PostType { get; set; }
        public int LikesCount { get; set; }
        public bool LikedByClient { get; set; }
        public PostCommentDto[] PostComments { get; set; }
        public PostFileDto[] Files { get; set; }

        public PostDto(Guid id, ProfileDto author, string text, DateTime creationTime, IEnumerable<PostFileDto> files, int numberOfFiles, string? postType, int likesCount, bool likedByClient, PostCommentDto[] postComments)
        {
            Id = id;
            Author = author;
            Text = text;
            CreationTime = creationTime;
            NumberOfFiles = numberOfFiles;
            PostType = postType;
            LikesCount = likesCount;
            LikedByClient = likedByClient;
            PostComments = postComments;
            Files = files.ToArray();
        }
    }
}
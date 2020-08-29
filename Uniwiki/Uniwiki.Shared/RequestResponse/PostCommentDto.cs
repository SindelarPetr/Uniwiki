using System;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class PostCommentDto
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public int LikesCount { get; set; }
        public string AuthorUrl { get; }
        public string AuthorProfilePictureSrc { get; }
        public Guid AuthorId { get; }
        public string AuthorFullName { get; }
        public string AuthorNameIdentifier { get; }
        public string Text { get; set; }
        public bool LikedByClient { get; set; }

        public PostCommentDto(Guid id, DateTime creationTime, int likesCount, string authorUrl, string text, bool likedByClient, string authorProfilePictureSrc, string authorNameIdentifier, string authorFullName, Guid authorId)
        {
            Id = id;
            CreationTime = creationTime;
            LikesCount = likesCount;
            AuthorUrl = authorUrl;
            Text = text;
            LikedByClient = likedByClient;
            AuthorProfilePictureSrc = authorProfilePictureSrc;
            AuthorNameIdentifier = authorNameIdentifier;
            AuthorFullName = authorFullName;
            AuthorId = authorId;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Uniwiki.Shared.RequestResponse;

namespace Uniwiki.Shared.ModelDtos
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public string AuthorUrl { get; }
        public Guid AuthorId { get; }
        public string AuthorFullName { get; }
        public string AuthorNameIdentifier { get; }
        public string AuthorProfilePictureSrc { get; }
        public int PostNumber { get; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public int NumberOfFiles { get; set; }
        public string? PostType { get; set; }
        public int LikesCount { get; set; }
        public bool LikedByClient { get; set; }
        public PostCommentDto[] PostComments { get; set; }
        public PostFileDto[] Files { get; set; }

        public PostViewModel(Guid id, string authorUrl, string text, DateTime creationTime, IEnumerable<PostFileDto> files, int numberOfFiles, string? postType, int likesCount, bool likedByClient, PostCommentDto[] postComments, Guid authorId, string authorFullName, string authorNameIdentifier, string authorProfilePictureSrc, int postNumber)
        {
            Id = id;
            AuthorUrl = authorUrl;
            Text = text;
            CreationTime = creationTime;
            NumberOfFiles = numberOfFiles;
            PostType = postType;
            LikesCount = likesCount;
            LikedByClient = likedByClient;
            PostComments = postComments;
            Files = files.ToArray();
            AuthorId = authorId;
            AuthorFullName = authorFullName;
            AuthorNameIdentifier = authorNameIdentifier;
            AuthorProfilePictureSrc = authorProfilePictureSrc;
            PostNumber = postNumber;
        }
    }
}
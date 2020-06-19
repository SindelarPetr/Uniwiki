using System;
using Uniwiki.Shared.ModelDtos;

namespace Uniwiki.Shared.RequestResponse
{
    public class PostCommentDto
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public int LikesCount { get; set; }
        public ProfileDto Profile { get; set; }
        public string Text { get; set; }
        public bool LikedByClient { get; set; }

        public PostCommentDto(Guid id, DateTime creationTime, int likesCount, ProfileDto profile, string text, bool likedByClient)
        {
            Id = id;
            CreationTime = creationTime;
            LikesCount = likesCount;
            Profile = profile;
            Text = text;
            LikedByClient = likedByClient;
        }
    }
}
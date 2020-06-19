using System;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostCommentLikeModel
    {
        public PostCommentModel Comment { get; }
        public ProfileModel Profile { get; }
        public DateTime LikeTime { get; }
        public bool IsRemoved { get; set; }

        public PostCommentLikeModel(PostCommentModel comment, ProfileModel profile, DateTime likeTime, bool isRemoved = false)
        {
            Comment = comment;
            Profile = profile;
            LikeTime = likeTime;
            IsRemoved = isRemoved;
        }

        public void Remove()
        {
            IsRemoved = true;
        }
    }
}
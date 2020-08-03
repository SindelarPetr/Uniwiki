using System;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostCommentLikeModel
    {
        public PostCommentModel Comment { get; }
        public ProfileModel Profile { get; }
        public DateTime LikeTime { get; }
        public bool IsRemoved { get; set; }

        internal PostCommentLikeModel()
        {

        }

        internal PostCommentLikeModel(PostCommentModel comment, ProfileModel profile, DateTime likeTime, bool isRemoved = false)
        {
            Comment = comment;
            Profile = profile;
            LikeTime = likeTime;
            IsRemoved = isRemoved;
        }

        internal void Remove()
        {
            IsRemoved = true;
        }
    }
}
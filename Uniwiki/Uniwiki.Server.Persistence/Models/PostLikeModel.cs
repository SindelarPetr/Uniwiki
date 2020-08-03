using System;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostLikeModel
    {
        public PostModel Post { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public DateTime DateTime { get; protected set; }
        public bool IsRemoved { get; protected set; }

        internal PostLikeModel()
        {

        }

        internal PostLikeModel(PostModel post, ProfileModel profile, DateTime dateTime, bool isRemoved = false)
        {
            Post = post;
            Profile = profile;
            DateTime = dateTime;
            IsRemoved = isRemoved;
        }

        internal void Removed() => IsRemoved = true;
    }
}
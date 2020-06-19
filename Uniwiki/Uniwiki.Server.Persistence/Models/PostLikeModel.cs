using System;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostLikeModel
    {
        public PostModel Post { get; }
        public ProfileModel Profile { get; }
        public DateTime DateTime { get; }
        public bool IsRemoved { get; private set; }

        public PostLikeModel(PostModel post, ProfileModel profile, DateTime dateTime, bool isRemoved = false)
        {
            Post = post;
            Profile = profile;
            DateTime = dateTime;
            IsRemoved = isRemoved;
        }

        public void Removed() => IsRemoved = true;
    }
}
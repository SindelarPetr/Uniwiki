using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostLikeModel : IIdModel<Guid[]>
    {
        public PostModel Post { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public DateTime DateTime { get; protected set; }
        public bool IsLiked { get; protected set; }

        public Guid[] Id => new Guid[] { Post.Id, Profile.Id };

        internal PostLikeModel(PostModel post, ProfileModel profile, DateTime dateTime, bool isLiked)
        {
            Post = post;
            Profile = profile;
            DateTime = dateTime;
            IsLiked = isLiked;
        }

        internal PostLikeModel()
        {

        }
    }
}
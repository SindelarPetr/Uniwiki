using System;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostLikeModel : IIdModel<Guid[]>
    {
        public static Guid[] GetId(PostModel post, ProfileModel profile) => new Guid[0];

        public Guid PostId { get; protected set; }
        public PostModel Post { get; protected set; }
        public Guid ProfileId { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public DateTime DateTime { get; protected set; }
        public bool IsLiked { get; protected set; }

        public Guid[] Id => GetId(Post, Profile);

        // Keep it internal - its created in a repository method
        internal PostLikeModel(PostModel post, ProfileModel profile, DateTime dateTime, bool isLiked)
        {
            Post = post;
            Profile = profile;
            DateTime = dateTime;
            IsLiked = isLiked;
        }

        protected PostLikeModel()
        {

        }

        internal void Like() => IsLiked = true;
        
        internal void Unlike() => IsLiked = false;
    }
}
using System;
using Uniwiki.Server.Persistence.ModelIds;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostLikeModel : ModelBase<PostLikeModelId>
    {
        public Guid PostId { get; protected set; }
        public PostModel Post { get; protected set; } = null!;
        public Guid ProfileId { get; protected set; }
        public ProfileModel Profile { get; protected set; } = null!;
        public DateTime DateTime { get; protected set; }
        public bool IsLiked { get; protected set; }

        // Keep it internal - its created in a repository method
        internal PostLikeModel(PostModel post, ProfileModel profile, DateTime dateTime, bool isLiked)
            : base(new PostLikeModelId(post.Id, profile.Id))
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
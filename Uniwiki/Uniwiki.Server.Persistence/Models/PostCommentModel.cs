using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostCommentModel : RemovableModelBase<Guid>
    {
        public Guid PostId { get; protected set; }
        public PostModel Post { get; protected set; } = null!;
        public Guid ProfileId { get; protected set; }
        public ProfileModel Profile { get; protected set; } = null!;
        public string Text { get; protected set; } = null!;
        public DateTime CreationTime { get; protected set; }
        public ICollection<PostCommentLikeModel> Likes { get; protected set; }
            = new List<PostCommentLikeModel>();

        internal PostCommentModel(Guid id, ProfileModel profile, PostModel post, string text, DateTime creationTime, bool isRemoved)
            : base(isRemoved, id)
        {
            PostId = post.Id;
            Post = post;
            ProfileId = profile.Id;
            Profile = profile;
            Text = text;
            CreationTime = creationTime;
        }

        protected PostCommentModel()
        {

        }

        internal void Edit(string text) => Text = text;
    }
}
using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostCommentModel : RemovableModelBase<Guid>
    {
        public PostModel Post { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public string Text { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        public IEnumerable<PostCommentLikeModel> Likes { get; protected set; }

        internal PostCommentModel(Guid id, ProfileModel profile, PostModel post, string text, DateTime creationTime, bool isRemoved)
            : base(isRemoved, id)
        {
            Post = post;
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
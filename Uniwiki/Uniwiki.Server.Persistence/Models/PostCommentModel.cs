using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostCommentModel : IRemovableModel, IIdModel<Guid>
    {
        public PostModel Post { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public string Text { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        public IEnumerable<PostCommentLikeModel> Likes { get; protected set; }
        public Guid Id { get; protected set; }

        bool IRemovableModel.IsRemoved { get; set; }

        public PostCommentModel(Guid id, ProfileModel profile, PostModel post, string text, DateTime creationTime, bool isRemoved)
        {
            Id = id;
            Post = post;
            Profile = profile;
            Text = text;
            CreationTime = creationTime;
            ((IRemovableModel)this).IsRemoved = isRemoved;
        }

        protected PostCommentModel()
        {

        }

        internal void Edit(string text) => Text = text;
    }
}
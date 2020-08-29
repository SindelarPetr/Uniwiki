using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostCommentModel : RemovableModelBase<Guid>
    {
        public Guid PostId { get; protected set; }
        public PostModel Post { get; protected set; } = null!;
        public Guid AuthorId { get; protected set; }
        public ProfileModel Author { get; protected set; } = null!;
        public string Text { get; protected set; } = null!;
        public DateTime CreationTime { get; protected set; }
        public ICollection<PostCommentLikeModel> Likes { get; protected set; }
            = new List<PostCommentLikeModel>();

        internal PostCommentModel(Guid id, Guid authorId, Guid postId, string text, DateTime creationTime, bool isRemoved)
            : base(isRemoved, id)
        {
            PostId = postId;
            AuthorId = authorId;
            Text = text;
            CreationTime = creationTime;
        }

        protected PostCommentModel()
        {

        }

        internal void Edit(string text) => Text = text;
    }
}
using System;
using System.Collections.Generic;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostCommentModel
    {
        public Guid Id { get; protected set; }
        public PostModel Post { get; protected set; }
        public ProfileModel Profile { get; protected set; }
        public string Text { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        public bool IsRemoved { get; protected set; }
        public IEnumerable<PostCommentLikeModel> Likes { get; protected set; }

        internal PostCommentModel()
        {

        }

        internal PostCommentModel(Guid id, ProfileModel profile, PostModel post, string text, DateTime creationTime, IEnumerable<PostCommentLikeModel> likes, bool isRemoved = false)
        {
            Id = id;
            Post = post;
            Profile = profile;
            Text = text;
            CreationTime = creationTime;
            Likes = likes;
            IsRemoved = isRemoved;
        }

        internal void Remove() => IsRemoved = true;

        internal void Edit(string text) => Text = text;
    }
}
using System;
using System.Collections.Generic;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostCommentModel
    {
        public Guid Id { get; }
        public PostModel Post { get; }
        public ProfileModel Profile { get; }
        public string Text { get; private set; }
        public DateTime CreationTime { get; }
        public bool IsRemoved { get; private set; }
        public IEnumerable<PostCommentLikeModel> Likes { get; }

        public PostCommentModel(Guid id, ProfileModel profile, PostModel post, string text, DateTime creationTime, IEnumerable<PostCommentLikeModel> likes, bool isRemoved = false)
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
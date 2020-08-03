using System;
using System.Collections.Generic;
using System.Linq;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostModel
    {
        public Guid Id { get; protected set; }
        public string? PostType { get; protected set; }
        public ProfileModel Author { get; protected set; }
        public string Text { get; protected set; }
        public CourseModel Course { get; protected set; }
        public DateTime CreationTime { get; protected set; }

        public PostFileModel[] Files { get; protected set; }

        public IEnumerable<PostLikeModel> Likes { get; protected set; }
        public IEnumerable<PostCommentModel> Comments { get; protected set; }

        public bool IsRemoved { get; protected set; }

        internal PostModel()
        {

        }

        internal PostModel(Guid id, string? postType, ProfileModel author, string text, CourseModel course, DateTime creationTime, IEnumerable<PostFileModel> files, IEnumerable<PostCommentModel> comments, IEnumerable<PostLikeModel> likes, bool isRemoved = false)
        {
            Id = id;
            PostType = postType;
            Author = author;
            Text = text;
            Course = course;
            CreationTime = creationTime;
            Comments = comments;
            Likes = likes;
            IsRemoved = isRemoved;
            Files = files.ToArray();
        }

        internal void Edit(string text, string? postType, PostFileModel[] postFiles)
        {
            Text = text;
            PostType = postType;
            Files = postFiles;
        }

        public void Remove()
        {
            IsRemoved = true;
        }
    }
}

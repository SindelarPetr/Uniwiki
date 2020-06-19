using System;
using System.Collections.Generic;
using System.Linq;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostModel
    {
        public Guid Id { get; }
        public string? PostType { get; private set; } // Setter for editting
        public ProfileModel Author { get; }
        public string Text { get; private set; } // Setter for editting
        public CourseModel Course { get; }
        public DateTime CreationTime { get; }

        public PostFileModel[] Files { get; private set; } // Setter for editting

        public IEnumerable<PostLikeModel> Likes { get; private set; }
        public IEnumerable<PostCommentModel> Comments { get; private set; }

        public bool IsRemoved { get; private set; }

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

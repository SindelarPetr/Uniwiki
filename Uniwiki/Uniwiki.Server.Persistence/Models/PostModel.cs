using System;
using System.Collections.Generic;
using System.Linq;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostModel : IIdModel<Guid>, IRemovableModel
    {
        public string? PostType { get; protected set; }
        public ProfileModel Author { get; protected set; }
        public string Text { get; protected set; }
        public CourseModel Course { get; protected set; }
        public DateTime CreationTime { get; protected set; }

        public PostFileModel[] Files { get; protected set; }

        public IEnumerable<PostLikeModel> Likes { get; protected set; }
        public IEnumerable<PostCommentModel> Comments { get; protected set; }
        bool IRemovableModel.IsRemoved { get; set; }

        public Guid Id { get; protected set; }

        public PostModel(Guid id, string? postType, ProfileModel author, string text, CourseModel course, DateTime creationTime, IEnumerable<PostFileModel> files, bool isRemoved)
        {
            Id = id;
            PostType = postType;
            Author = author;
            Text = text;
            Course = course;
            CreationTime = creationTime;
            ((IRemovableModel)this).IsRemoved = isRemoved;
            Files = files.ToArray();
        }

        protected PostModel()
        {

        }

        internal void Edit(string text, string? postType, PostFileModel[] postFiles)
        {
            Text = text;
            PostType = postType;
            Files = postFiles;
        }
    }
}

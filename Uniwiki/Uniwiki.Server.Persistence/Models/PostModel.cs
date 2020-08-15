﻿using System;
using System.Collections.Generic;
using System.Linq;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostModel : RemovableModelBase<Guid>
    {
        public string? PostType { get; protected set; }
        public ProfileModel Author { get; protected set; }
        public string Text { get; protected set; }
        public CourseModel Course { get; protected set; }
        public DateTime CreationTime { get; protected set; }

        public PostFileModel[] PostFiles { get; protected set; }

        public ICollection<PostLikeModel> Likes { get; protected set; }
            = new List<PostLikeModel>();
        
        public ICollection<PostCommentModel> Comments { get; protected set; }
            = new List<PostCommentModel>();

        internal PostModel(Guid id, string? postType, ProfileModel author, string text, CourseModel course, DateTime creationTime, bool isRemoved) : base(isRemoved, id)
        {
            PostType = postType;
            Author = author;
            Text = text;
            Course = course;
            CreationTime = creationTime;
        }

        protected PostModel()
        {

        }

        internal void Edit(string text, string? postType, PostFileModel[] postFiles)
        {
            Text = text;
            PostType = postType;
            PostFiles = postFiles;
        }

        internal void SetPostFiles(PostFileModel[] postFileModels)
        {
            PostFiles = postFileModels;
        }
    }
}

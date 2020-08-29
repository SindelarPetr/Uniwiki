﻿using System;
using Uniwiki.Server.Persistence.ModelIds;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostCommentLikeModel : ModelBase<PostCommentLikeModelId>
    {
        public Guid CommentId { get; set; } 
        public PostCommentModel Comment { get; set; } = null!;
        public Guid ProfileId { get; set; }
        public ProfileModel Profile { get; set; } = null!;
        public DateTime LikeTime { get; protected set; }
        public bool IsLiked { get; protected set; }

        internal PostCommentLikeModel(Guid commentId, Guid profileId, DateTime likeTime, bool isLiked)
            : base(new PostCommentLikeModelId(commentId, profileId))
        {
            CommentId = commentId;
            ProfileId = profileId;
            LikeTime = likeTime;
            IsLiked = isLiked;
        }

        protected PostCommentLikeModel()
        {

        }

        internal void Like() => IsLiked = true;
        internal void Unlike() => IsLiked = false;
    }
}
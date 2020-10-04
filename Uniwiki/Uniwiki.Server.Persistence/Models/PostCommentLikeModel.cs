using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uniwiki.Server.Persistence.ModelIds;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostCommentLikeModel
    {
        public class Map : IEntityTypeConfiguration<PostCommentLikeModel>
        {
            public void Configure(EntityTypeBuilder<PostCommentLikeModel> builder)
                => builder.HasKey(e => new PostCommentLikeModelId(e.CommentId, e.ProfileId));
        }

        public Guid CommentId { get; set; } 
        public PostCommentModel Comment { get; set; } = null!;
        public Guid ProfileId { get; set; }
        public ProfileModel Profile { get; set; } = null!;
        public DateTime LikeTime { get; set; }
        public bool IsLiked { get; set; }
        internal void Like() => IsLiked = true;
        internal void Unlike() => IsLiked = false;

        public PostCommentLikeModel(Guid commentId, Guid profileId, DateTime likeTime, bool isLiked)
        {
            CommentId = commentId;
            ProfileId = profileId;
            LikeTime = likeTime;
            IsLiked = isLiked;
        }

        protected PostCommentLikeModel()
        {

        }
    }
}
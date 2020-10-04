using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uniwiki.Server.Persistence.ModelIds;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostLikeModel
    {
        public class Map : IEntityTypeConfiguration<PostLikeModel>
        {
            public void Configure(EntityTypeBuilder<PostLikeModel> builder) 
                => builder.HasKey(m => new PostLikeModelId(m.PostId, m.ProfileId));
        }

        public Guid PostId { get; set; }
        public PostModel Post { get; set; } = null!;
        public Guid ProfileId { get; set; }
        public ProfileModel Profile { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public bool IsLiked { get; set; }

        // Keep it internal - its created in a repository method
        public PostLikeModel(Guid postId, Guid profileId, DateTime dateTime, bool isLiked)
        {
            PostId = postId;
            ProfileId = profileId;
            DateTime = dateTime;
            IsLiked = isLiked;
        }

        internal void Like() => IsLiked = true;
        
        internal void Unlike() => IsLiked = false;
    }
}
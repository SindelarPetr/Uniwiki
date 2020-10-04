using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uniwiki.Server.Persistence.Models.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public interface ISoftDeletable
    {

    }

    public class PostCommentModel : ISoftDeletable
    {
        public class Map : IEntityTypeConfiguration<PostCommentModel>
        {
            public void Configure(EntityTypeBuilder<PostCommentModel> builder)
            {
                // Soft-deleting
                builder.Property<int>(PersistenceConstants.IsDeleted);
                builder.HasQueryFilter(m => EF.Property<int>(m, PersistenceConstants.IsDeleted) == 0);

                builder
                    .HasMany(m => m.Likes)
                    .WithOne(l => l.Comment)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public PostModel Post { get; set; } = null!;
        public Guid AuthorId { get; set; }
        public ProfileModel Author { get; set; } = null!;
        // Max length
        public string Text { get; set; } = null!;
        public DateTime CreationTime { get; set; }
        public ICollection<PostCommentLikeModel> Likes { get; set; } = null!;

        public PostCommentModel(Guid id, Guid authorId, Guid postId, string text, DateTime creationTime)
        {
            Id = id;
            PostId = postId;
            AuthorId = authorId;
            Text = text;
            CreationTime = creationTime;
            Likes = new List<PostCommentLikeModel>();
        }

        protected PostCommentModel()
        {

        }

        internal void Edit(string text) => Text = text;
    }
}
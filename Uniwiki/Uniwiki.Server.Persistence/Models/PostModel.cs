using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Toolbelt.ComponentModel.DataAnnotations.Schema.V5;
using Uniwiki.Server.Persistence.Models.Base;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Shared;

namespace Uniwiki.Server.Persistence.Models
{
    public class PostModel : ModelBase<Guid>, ISoftDeletable
    {
        public class Map : IEntityTypeConfiguration<PostModel>
        {
            public void Configure(EntityTypeBuilder<PostModel> builder)
            {
                builder.Property<int>(PersistenceConstants.IsDeleted);
                builder.HasQueryFilter(m => EF.Property<int>(m, PersistenceConstants.IsDeleted) == 0);

                builder
                    .HasMany(m => m.Comments)
                    .WithOne(c => c.Post)
                    .HasForeignKey(c => c.PostId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasIndex(m => m.CreationTime);
            }
        }

        [IndexColumn]
        [MaxLength(Constants.Validations.PostTypeMaxLength)]
        public string? PostType { get; set; }
        public Guid AuthorId { get; set; }
        public ProfileModel Author { get; set; } = null!;
        // Max text lenght
        public string Text { get; set; } = null!;

        public Guid CourseId { get; set; }
        public CourseModel Course { get; set; } = null!;

        [IndexColumn]
        public DateTime CreationTime { get; set; }

        public ICollection<PostFileModel> PostFiles { get; set; } = null!;

        public ICollection<PostLikeModel> Likes { get; set; } = null!;

        public ICollection<PostCommentModel> Comments { get; set; } = null!;

        public PostModel(Guid id, string? postType, Guid authorId, string text, Guid courseId, DateTime creationTime) : base(id)
        {
            PostType = postType;
            AuthorId = authorId;
            Text = text;
            CourseId = courseId;
            CreationTime = creationTime;
            PostFiles = new List<PostFileModel>();
            Likes = new List<PostLikeModel>();
            Comments = new List<PostCommentModel>();
        }

        protected PostModel()
        {

        }

        internal void Edit(string text, string? postType)
        {
            Text = text;
            PostType = postType;
        }
    }
}

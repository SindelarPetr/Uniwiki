using System;
using System.Collections.Generic;
using System.Text;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    // View
    public class FilterPostCategoryModel
    {
        public PostCategoryModelId Id { get; }
        public PostCategoryModel PostCategory { get; set; }
        public int PostsCount { get; set; }

        public FilterPostCategoryModel(PostCategoryModel postCategory)
        {
            PostCategory = postCategory;
        }
    }

    // View
    public class NewPostCategoryModel : IIdModel<PostCategoryModelId>
    {
        public PostCategoryModelId Id { get; }
        public PostCategoryModel PostCategory { get; set; }

        public NewPostCategoryModel(PostCategoryModel postCategory)
        {
            PostCategory = postCategory;
        }
    }


    public class PostCategoryModel : IIdModel<PostCategoryModelId>
    {
        public PostCategoryModelId Id => new PostCategoryModelId(Name, CourseId);

        public string Name { get; protected set; }

        public Guid CourseId { get; protected set; }
        
        public CourseModel Course { get; protected set; }

        public ICollection<PostModel> Posts { get; set; } = new List<PostModel>();

        public PostCategoryModel(string name, Guid courseId)
        {
            Name = name;
            CourseId = courseId;
        }
    }
}
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
    //public class NewPostCategoryModel : IdModel<PostCategoryModelId>
    //{
    //    public PostCategoryModelId Id { get; }
    //    public PostCategoryModel PostCategory { get; set; }

    //    public NewPostCategoryModel(PostCategoryModel postCategory)
    //        : base(new PostCategoryModelId(postCategory.Name, postCategory.CourseId))
    //    {
    //        PostCategory = postCategory;
    //    }
    //}


    public class PostCategoryModel : ModelBase<PostCategoryModelId>
    {
        public PostCategoryModelId Id => new PostCategoryModelId(Name, CourseId);

        public string Name { get; protected set; }

        public Guid CourseId { get; protected set; }
        
        public CourseModel Course { get; protected set; }

        public ICollection<PostModel> Posts { get; set; } = new List<PostModel>();

        public PostCategoryModel(string name, Guid courseId)
            : base(new PostCategoryModelId(name, courseId))
        {
            Name = name;
            CourseId = courseId;
        }
    }
}
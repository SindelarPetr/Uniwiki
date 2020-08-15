using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class CourseModel : RemovableModelBase<Guid>
    {
        public string Code { get; protected set; } = null!;
        public string FullName { get; protected set; } = null!;
        public Guid StudyGroupId { get; protected set; }
        public StudyGroupModel StudyGroup { get; protected set; } = null!;
        public Guid AuthorId { get; protected set; }
        public ProfileModel Author { get; protected set; } = null!;
        public string Url { get; protected set; } = null!;
        public ICollection<PostModel> Posts { get; protected set; }
            = new List<PostModel>();

        internal CourseModel(Guid id, string code, string fullname, StudyGroupModel studyGroup, ProfileModel author, string url, bool isRemoved)
            : base(isRemoved, id)
        {
            Code = code;
            FullName = fullname;
            StudyGroupId = studyGroup.Id;
            StudyGroup = studyGroup;
            AuthorId = author.Id;
            Author = author;
            Url = url;
        }

        protected CourseModel()
        {

        }
    }
}
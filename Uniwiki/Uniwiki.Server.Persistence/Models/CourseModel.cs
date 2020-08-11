using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class CourseModel : IIdModel<Guid>, IRemovableModel
    {
        public string Code { get; protected set; }
        public string FullName { get; protected set; }
        public StudyGroupModel StudyGroup { get; protected set; }
        public ProfileModel Author { get; protected set; }
        public string Url { get; protected set; }
        public IEnumerable<PostModel> Posts { get; protected set; }
        public Guid Id { get; protected set; }
        bool IRemovableModel.IsRemoved { get; set; }

        public CourseModel(Guid id, string code, string fullname, StudyGroupModel studyGroup, ProfileModel author, string url, bool isRemoved)
        {
            Id = id;
            Code = code;
            FullName = fullname;
            StudyGroup = studyGroup;
            Author = author;
            Url = url;
            ((IRemovableModel)this).IsRemoved = isRemoved;
        }

        protected CourseModel()
        {

        }
    }
}
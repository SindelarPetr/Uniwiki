using System;
using System.Collections.Generic;

namespace Uniwiki.Server.Persistence.Models
{
    public class CourseModel
    {
        public string Code { get; protected set; }
        public string FullName { get; protected set; }
        public StudyGroupModel StudyGroup { get; protected set; }
        public ProfileModel Author { get; protected set; }
        public string Url { get; protected set; }
        public Guid Id { get; protected set; }
        public IEnumerable<PostModel> Posts { get; protected set; }

        internal CourseModel()
        {

        }

        internal CourseModel(Guid id, string code, string fullname, StudyGroupModel studyGroup, ProfileModel author, string url, IEnumerable<PostModel> posts)
        {
            Id = id;
            Code = code;
            FullName = fullname;
            StudyGroup = studyGroup;
            Author = author;
            Url = url;
            Posts = posts;
        }
    }
}
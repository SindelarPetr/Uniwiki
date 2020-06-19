using System;
using System.Collections.Generic;

namespace Uniwiki.Server.Persistence.Models
{
    public class CourseModel
    {
        public string Code { get; }
        public string FullName { get; }
        public StudyGroupModel StudyGroup { get; }
        public ProfileModel Author { get; }
        public string Url { get; private set; }
        public Guid Id { get; }
        public IEnumerable<PostModel> Posts { get;}
        public IEnumerable<string?> PostTypes { get; } 

        internal CourseModel(Guid id, string code, string fullname, StudyGroupModel studyGroup, ProfileModel author, string url, IEnumerable<PostModel> posts, IEnumerable<string?> postTypes)
        {
            Id = id;
            Code = code;
            FullName = fullname;
            StudyGroup = studyGroup;
            Author = author;
            Url = url;
            Posts = posts;
            PostTypes = postTypes;
        }
    }
}
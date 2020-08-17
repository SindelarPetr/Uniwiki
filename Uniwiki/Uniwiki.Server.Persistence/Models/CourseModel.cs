using System;
using System.Collections.Generic;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class CourseModel : RemovableModelBase<Guid>
    {
        public string Code { get; protected set; } = null!;
        public string CodeStandardized { get; protected set; } = null!;
        public string FullName { get; protected set; } = null!;
        public string FullNameStandardized { get; protected set; } = null!;
        public Guid StudyGroupId { get; protected set; }
        public StudyGroupModel StudyGroup { get; protected set; } = null!;
        public Guid AuthorId { get; protected set; }
        public ProfileModel Author { get; protected set; } = null!;
        public string Url { get; protected set; } = null!;
        public string StudyGroupUrl { get; protected set; } = null!;
        public string UniversityUrl { get; protected set; } = null!;
        public ICollection<PostModel> Posts { get; protected set; }
            = new List<PostModel>();

        internal CourseModel(Guid id, string code, string codeStandardized, string fullname, string fullnameStandardized, ProfileModel author, StudyGroupModel studyGroup, string universityUrl, string url, bool isRemoved)
            : base(isRemoved, id)
        {
            Code = code;
            CodeStandardized = codeStandardized;
            FullName = fullname;
            FullNameStandardized = fullnameStandardized;
            AuthorId = author.Id;
            // Author = author;
            StudyGroupId = studyGroup.Id;
            StudyGroup = studyGroup;
            Url = url;
            StudyGroupUrl = studyGroup.Url;
            UniversityUrl = universityUrl;
        }

        protected CourseModel()
        {

        }
    }
}
using System;

namespace Uniwiki.Server.WebHost.Controllers
{
    public class PostViewModel
    {
        public PostViewModel(string text, string category, bool likedByUser, int likesCount, DateTime creationTime, PostCommentViewModel[] postComments, string authorName, string authorUrl, string courseName, PostFileItemViewModel[] postFiles, string universityName, string universityUrl, string facultyName, string facultyUrl)
        {
            Text = text;
            Category = category;
            LikedByUser = likedByUser;
            LikesCount = likesCount;
            CreationTime = creationTime;
            Comments = postComments;
            AuthorName = authorName;
            AuthorUrl = authorUrl;
            CourseName = courseName;
            PostFiles = postFiles;
            UniversityName = universityName;
            UniversityUrl = universityUrl;
            FacultyName = facultyName;
            FacultyUrl = facultyUrl;
        }

        public string Text { get; }
        public string Category { get; }
        public bool LikedByUser { get; }
        public int LikesCount { get; }
        public DateTime CreationTime { get; }
        public PostCommentViewModel[] Comments { get; }
        public string AuthorName { get; }
        public string AuthorUrl { get; }
        public string CourseName { get; }
        public PostFileItemViewModel[] PostFiles { get; }
        public string UniversityName { get; }
        public string UniversityUrl { get; }
        public string FacultyName { get; }
        public string FacultyUrl { get; }
    }
}
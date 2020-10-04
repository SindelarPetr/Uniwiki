using System;

namespace Uniwiki.Server.WebHost.Controllers
{
    public class PostItemViewModel
    {
        public PostItemViewModel(string text, string category, PostFileItemViewModel[] postFiles, PostAuthorItemViewModel postAuthor, DateTime creationTime, bool likedByUser, int likesCount, int commentsCount, string postUrl)
        {
            Text = text;
            Category = category;
            PostFiles = postFiles;
            PostAuthor = postAuthor;
            CreationTime = creationTime;
            LikedByUser = likedByUser;
            LikesCount = likesCount;
            CommentsCount = commentsCount;
            Url = postUrl;
        }

        public string Text { get; }
        public string Category { get; }
        public PostFileItemViewModel[] PostFiles { get; }
        public PostAuthorItemViewModel PostAuthor { get; }
        public DateTime CreationTime { get; }
        public bool LikedByUser { get; }
        public int LikesCount { get; }
        public int CommentsCount { get; }
        public string Url { get; }
    }
}
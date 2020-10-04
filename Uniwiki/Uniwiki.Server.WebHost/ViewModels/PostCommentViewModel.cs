using System;

namespace Uniwiki.Server.WebHost.Controllers
{
    public class PostCommentViewModel
    {
        public PostCommentViewModel(string authorName, string authorUrl, DateTime creationDate, string text, int likesCount, bool likedByUser)
        {
            AuthorName = authorName;
            AuthorUrl = authorUrl;
            CreationTime = creationDate;
            Text = text;
            LikesCount = likesCount;
            LikedByUser = likedByUser;
        }

        public string AuthorName { get; }
        public string AuthorUrl { get; }
        public DateTime CreationTime { get; }
        public string Text { get; }
        public int LikesCount { get; }
        public bool LikedByUser { get; }
    }
}
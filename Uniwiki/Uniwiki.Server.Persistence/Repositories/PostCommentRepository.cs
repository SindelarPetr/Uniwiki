using System;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    public class PostCommentRepository : RemovableRepositoryBase<PostCommentModel, Guid> 
    {
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.Error_PostCommentNotFound;

        public PostCommentRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.PostComments)
        {
            _textService = textService;
        }

        public PostCommentModel EditComment(PostCommentModel comment, string text)
        {
            comment.Edit(text);

            return comment;
        }

        public Guid AddPostComment(Guid authorId, Guid postId, string commentText, DateTime creationTime)
        {
            var postComment = new PostCommentModel(Guid.NewGuid(), authorId, postId, commentText, creationTime, false);

            All.Add(postComment);

            SaveChanges();

            return postComment.Id;
        }
    }
}

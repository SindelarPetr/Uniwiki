using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions;
using Uniwiki.Server.Persistence.RepositoryAbstractions.Base;
using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    internal class PostCommentRepository : RepositoryBase<PostCommentModel>, IPostCommentRepository
    {
        private readonly TextService _textService;

        public string NotFoundByIdMessage => _textService.Error_PostCommentNotFound;

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

        public PostCommentModel AddPostComment(ProfileModel profile, PostModel post, string commentText, DateTime creationTime)
        {
            var postComment = new PostCommentModel(Guid.NewGuid(), profile, post, commentText, creationTime, false);

            All.Add(postComment);

            return postComment;
        }
    }
}

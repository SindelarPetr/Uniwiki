using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;

using Uniwiki.Server.Persistence.Services;

namespace Uniwiki.Server.Persistence.Repositories
{
    public class FeedbackRepository : RepositoryBase<FeedbackModel, Guid> 
    {
        private readonly TextService _textService;

        public override string NotFoundByIdMessage => _textService.Error_FeedbackNotFound;

        public FeedbackRepository(UniwikiContext uniwikiContext, TextService textService)
            : base(uniwikiContext, uniwikiContext.Feedbacks)
        {
            _textService = textService;
        }

        public double? GetAverageRating() 
            => All.Where(f => f.Rating != null).Average(f => f.Rating);

        public IEnumerable<string> GetLastFeedbacks(int count) 
            => All.Select(f =>
                $"{(f.Rating.HasValue ? $"{f.CreationTime.ToString("g")} ({f.Rating}) " : string.Empty)}{f.Text}\n").Reverse().Take(count).Reverse();

        public int GetFeedbacksCount() 
            => All.Count();

        public int GetTextOnlyFeedbacksCount() 
            => All.Count(f => f.Rating == null && !string.IsNullOrWhiteSpace(f.Text));

        public int RatingOnlyFeedbacksCount() 
            => All.Count(f => f.Rating != null && string.IsNullOrWhiteSpace(f.Text));

        public int TextAndRatingFeedbacksCount() 
            => All.Count(f => f.Rating != null && !string.IsNullOrWhiteSpace(f.Text));

        public FeedbackModel AddFeedback(Guid? userId, int? rating, string text, DateTime creationTime)
        {
            var feedback = new FeedbackModel(Guid.NewGuid(), userId, rating, text, creationTime);

            All.Add(feedback);

            return feedback;
        }
    }
}

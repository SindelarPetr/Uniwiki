using System;
using System.Collections.Generic;
using System.Linq;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Server.Persistence.RepositoryAbstractions;

namespace Uniwiki.Server.Persistence.Repositories
{
    class FeedbackRepository : IRemovableIdRepository<FeedbackModel>, IFeedbackRepository
    {
        private readonly UniwikiContext _uniwikiContext;

        public FeedbackRepository(UniwikiContext uniwikiContext)
        {
            _uniwikiContext = uniwikiContext;
        }

        public double? GetAverageRating()
        {
            return _uniwikiContext.Feedbacks.Where(f => f.Rating != null).Average(f => f.Rating);
        }

        public IEnumerable<string> GetLastFeedbacks(int count)
        {
            return _uniwikiContext.Feedbacks.Select(f =>
            $"{(f.Rating.HasValue ? $"{f.CreationTime.ToString("g")} ({f.Rating}) " : string.Empty)}{f.Text}\n").Reverse().Take(count).Reverse();
        }

        public int GetFeedbacksCount()
        {
            return _uniwikiContext.Feedbacks.Count();
        }

        public int GetTextOnlyFeedbacksCount()
        {
            return _uniwikiContext.Feedbacks.Count(f => f.Rating == null && !string.IsNullOrWhiteSpace(f.Text));
        }

        public int RatingOnlyFeedbacksCount()
        {
            return _uniwikiContext.Feedbacks.Count(f => f.Rating != null && string.IsNullOrWhiteSpace(f.Text));
        }

        public int TextAndRatingFeedbacksCount()
        {
            return _uniwikiContext.Feedbacks.Count(f => f.Rating != null && !string.IsNullOrWhiteSpace(f.Text));
        }
    }
}

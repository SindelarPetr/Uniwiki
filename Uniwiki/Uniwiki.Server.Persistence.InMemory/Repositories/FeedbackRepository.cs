using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uniwiki.Server.Persistence.InMemory.Services;
using Uniwiki.Server.Persistence.Models;
using Uniwiki.Server.Persistence.Repositories;

namespace Uniwiki.Server.Persistence.InMemory.Repositories
{
    class FeedbackRepository : IFeedbackRepository
    {
        private readonly DataService _dataService;

        public FeedbackRepository(DataService dataService)
        {
            _dataService = dataService;
        }

        public void CreateFeedback(ProfileModel? user, int? rating, string text, string ipAddress, DateTime creationTime)
        {
            var feedback = new FeedbackModel(user, rating, text, ipAddress, creationTime);

            _dataService.Feedbacks.Add(feedback);
        }

        public double? GetAverageRating()
        {
            return _dataService.Feedbacks.Where(f => f.Rating != null).Average(f => f.Rating);
        }

        public IEnumerable<string> GetLastFeedbacks(int count)
        {
            return _dataService.Feedbacks.Select(f =>
            $"{(f.Rating.HasValue ? $"{f.CreationTime.ToString("g")} ({f.Rating}) " : string.Empty)}{f.Text}\n").Reverse().Take(count).Reverse();
        }

        public int GetFeedbacksCount()
        {
            return _dataService.Feedbacks.Count();
        }

        public int GetTextOnlyFeedbacksCount()
        {
            return _dataService.Feedbacks.Count(f => f.Rating == null && !string.IsNullOrWhiteSpace(f.Text));
        }

        public int RatingOnlyFeedbacksCount()
        {
            return _dataService.Feedbacks.Count(f => f.Rating != null && string.IsNullOrWhiteSpace(f.Text));
        }

        public int TextAndRatingFeedbacksCount()
        {
            return _dataService.Feedbacks.Count(f => f.Rating != null && !string.IsNullOrWhiteSpace(f.Text));
        }
    }
}

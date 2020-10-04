using System;
using System.ComponentModel.DataAnnotations;
using Uniwiki.Server.Persistence.Models.Base;
using Uniwiki.Server.Persistence.Repositories.Base;
using Uniwiki.Shared;

namespace Uniwiki.Server.Persistence.Models
{
    public class FeedbackModel : ModelBase<Guid>
    {
        public Guid? UserId { get; set; }
        public ProfileModel? User { get; set; }
        public int? Rating { get; set; }
        // Lets keep max length for this text
        public string Text { get; set; } = null!;
        public DateTime CreationTime { get; set; }

        internal FeedbackModel(Guid id, Guid? userId, int? rating, string text, DateTime creationTime)
            :base(id)
        {
            UserId = userId;
            Rating = rating;
            Text = text;
            CreationTime = creationTime;
        }

        protected FeedbackModel()
        {

        }
    }
}

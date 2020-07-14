using System;
using System.Collections.Generic;
using System.Text;

namespace Uniwiki.Server.Persistence.Models
{
    public class FeedbackModel
    {
        public ProfileModel? User { get; }
        public int? Rating { get; }
        public string Text { get; }
        public string IpAddress { get; }
        public DateTime CreationTime { get; }
        
        public FeedbackModel(ProfileModel? user, int? rating, string text, string ipAddress, DateTime creationTime)
        {
            User = user;
            Rating = rating;
            Text = text;
            IpAddress = ipAddress;
            CreationTime = creationTime;
        }
    }
}

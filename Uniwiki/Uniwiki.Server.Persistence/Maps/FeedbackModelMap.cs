﻿using System;
using Uniwiki.Server.Persistence.Maps.Base;
using Uniwiki.Server.Persistence.Repositories.Base;

namespace Uniwiki.Server.Persistence.Models
{
    public class FeedbackModelMap : RemovableModelMapBase<FeedbackModel, Guid>
    {
        public FeedbackModelMap() : base("Feedbacks")
        {
        }
    }
}
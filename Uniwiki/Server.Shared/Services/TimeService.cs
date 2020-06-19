using System;
using Uniwiki.Shared.Services.Abstractions;

namespace Uniwiki.Shared.Services
{
    internal class TimeService : ITimeService
    {
        public DateTime Now => DateTime.Now;
    }
}

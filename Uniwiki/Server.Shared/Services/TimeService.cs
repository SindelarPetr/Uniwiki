using Shared.Services.Abstractions;
using System;

namespace Uniwiki.Shared.Services
{
    internal class TimeService : ITimeService
    {
        public DateTime Now => DateTime.Now;
    }
}

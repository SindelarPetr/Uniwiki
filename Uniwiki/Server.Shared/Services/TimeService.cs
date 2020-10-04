using System;
using Shared.Services.Abstractions;

namespace Shared.Services
{
    internal class TimeService : ITimeService
    {
        public DateTime Now => DateTime.Now;
    }
}

using Shared.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uniwiki.Shared.Tests.FakeServices
{
    public class FakeTimeService : ITimeService
    {
        public DateTime Now { get; private set; }

        public FakeTimeService(DateTime now)
        {
            Now = now;
        }

        public void SetNow(DateTime now) => Now = now;

        public void MoveTime(TimeSpan time) => Now += time;
    }
}

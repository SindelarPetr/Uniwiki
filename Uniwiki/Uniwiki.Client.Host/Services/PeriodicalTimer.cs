using System;
using System.Timers;
using Uniwiki.Client.Host.Services.Abstractions;

namespace Uniwiki.Client.Host.Services
{
    internal class PeriodicalTimer : IPeriodicalTimer
    {
        private TimeSpan _period;
        private int _periods;
        public int PeriodsLeft { get; private set; }
        public bool IsRunning => PeriodsLeft > 0;
        private Action _periodElapsed;
        private Timer _timer;

        public void Start(TimeSpan period, int periods, Action periodElapsed)
        {
            _period = period;
            PeriodsLeft = _periods = periods;
            _periodElapsed = periodElapsed;
            _timer = new Timer(_period.TotalMilliseconds);
            _timer.Elapsed += NotifyTimerElapsed;
            _timer.Enabled = true;
        }

        public void Stop()
        {
            _timer.Dispose();
        }

        private void NotifyTimerElapsed(Object source, ElapsedEventArgs e)
        {
            PeriodsLeft--;

            if(PeriodsLeft <= 0)
            {
                _timer.Stop();
                _timer.Interval = _period.TotalMilliseconds;
            }

            _periodElapsed?.Invoke();

            if(PeriodsLeft > 0)
            {
                _timer.Interval = _period.TotalMilliseconds;
                _timer.Start();
            }
        }
    }
}
using System;

namespace Uniwiki.Client.Host.Services.Abstractions
{
    public interface IPeriodicalTimer
    {
        int PeriodsLeft { get; }
        bool IsRunning { get; }
        void Start(TimeSpan period, int periods, Action periodElapsed);
        void Stop();
    }
}
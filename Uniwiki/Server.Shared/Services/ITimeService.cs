using System;

namespace Uniwiki.Shared.Services.Abstractions
{
    public interface ITimeService
    {
        DateTime Now { get; }
    }
}
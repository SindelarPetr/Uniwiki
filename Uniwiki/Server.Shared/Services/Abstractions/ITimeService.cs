using System;

namespace Shared.Services.Abstractions
{
    public interface ITimeService
    {
        DateTime Now { get; }
    }
}
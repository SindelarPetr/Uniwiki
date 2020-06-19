using System;
using System.Threading.Tasks;

namespace Uniwiki.Client.Host.Services.Abstractions
{
    public interface IScrollService
    {
        event Func<Task>? ScrolledToEnd;
        Task InitializeScroll();
    }
}
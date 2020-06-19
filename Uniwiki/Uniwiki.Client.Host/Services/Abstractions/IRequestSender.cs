using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlazorInputFile;
using Shared.RequestResponse;

namespace Uniwiki.Client.Host.Services.Abstractions
{
    public interface IRequestSender
    {
        Task<T> SendRequestAsync<T>(RequestBase<T> request, Action? finalAction = null,
            [CallerMemberName] string callerName = null, [CallerLineNumber] int lineNumber = 0) where T : ResponseBase;

        Task<T> SendRequestAsync<T>(RequestBase<T> request, IFileListEntry file, Action? finalAction = null,
            [CallerMemberName] string callerName = null, [CallerLineNumber] int lineNumber = 0) where T : ResponseBase;
    }
}
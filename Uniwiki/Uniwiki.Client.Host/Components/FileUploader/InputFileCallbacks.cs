using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Uniwiki.Client.Host.Components.FileUploader
{
    public sealed class InputFileCallbacks
    {
        public event Func<UploadFile, Task>? OnFileSelected;
        public event Action<string>? OnStart;
        public event Action<ProgressInfo>? OnProgress;
        public event Action<string>? OnError;
        public event Action<string>? OnAbort;
        public event Action<SuccessInfo>? OnSuccess;

        [JSInvokable(nameof(HandleFileSelected))]
        public void HandleFileSelected(UploadFile file)
        {
            OnFileSelected?.Invoke(file);
            Console.WriteLine(nameof(HandleFileSelected) + ": " + file.Name + " ");
        }

        [JSInvokable(nameof(HandleStart))]
        public void HandleStart(string fileId)
        {
            OnStart?.Invoke(fileId);
        }

        [JSInvokable(nameof(HandleProgress))]
        public void HandleProgress(ProgressInfo progressInfo)
        {
            OnProgress?.Invoke(progressInfo);
        }

        [JSInvokable(nameof(HandleError))]
        public void HandleError(string fileId)
        {
            OnError?.Invoke(fileId);
        }

        [JSInvokable(nameof(HandleAbort))]
        public void HandleAbort(string fileId)
        {
            OnAbort?.Invoke(fileId);
        }

        [JSInvokable(nameof(HandleSuccess))]
        public void HandleSuccess(SuccessInfo successInfo)
        {
            OnSuccess?.Invoke(successInfo);
        }
    }
}

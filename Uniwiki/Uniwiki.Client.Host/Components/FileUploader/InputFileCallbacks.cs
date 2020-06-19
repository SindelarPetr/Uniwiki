using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Uniwiki.Client.Host.Components.FileUploader
{
    public sealed class InputFileCallbacks
    {
        public event Func<UploadFile, Task>? OnFileSelected;
        public event Action<int>? OnStart;
        public event Action<ProgressInfo>? OnProgress;
        public event Action<int>? OnError;
        public event Action<int>? OnAbort;
        public event Action<SuccessInfo>? OnSuccess;

        [JSInvokable(nameof(HandleFileSelected))]
        public void HandleFileSelected(UploadFile file)
        {
            OnFileSelected?.Invoke(file);
            Console.WriteLine(nameof(HandleFileSelected) + ": " + file.Name + " ");
        }

        [JSInvokable(nameof(HandleStart))]
        public void HandleStart(int fileId)
        {
            OnStart?.Invoke(fileId);
        }

        [JSInvokable(nameof(HandleProgress))]
        public void HandleProgress(ProgressInfo progressInfo)
        {
            OnProgress?.Invoke(progressInfo);
        }

        [JSInvokable(nameof(HandleError))]
        public void HandleError(int fileId)
        {
            OnError?.Invoke(fileId);
        }

        [JSInvokable(nameof(HandleAbort))]
        public void HandleAbort(int fileId)
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

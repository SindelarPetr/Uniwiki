using System;
using System.Threading.Tasks;
using Uniwiki.Client.Host.Services.Abstractions;

namespace Uniwiki.Client.Host.Components.FileUploader
{
    public class UploadFile
    {
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }

        public bool Started { get; private set; }
        public double Progress { get; private set; }
        public bool Aborted { get; private set; }
        public bool Failed { get; private set; }
        public bool Succeeded { get; private set; }
        public bool Finished => Failed || Aborted || Succeeded;
        public bool Uploading => Started && !Finished;

        public event Action? OnStart;
        public event Action<double>? OnProgress;
        public event Action? OnFailed;
        public event Action<string>? OnSuccess;

        public IJsInteropService JsInteropService { get; set; }

        public void SetStart()
        {
            Started = true;
            OnStart?.Invoke();
        }

        public void SetProgress(double progress)
        {
            Progress = progress;
            OnProgress?.Invoke(progress);
        }

        public void SetSuccess(string dataForClient)
        {
            Succeeded = true;
            OnSuccess?.Invoke(dataForClient);
        }

        public void SetError()
        {
            Failed = true;
            OnFailed?.Invoke();
        }

        public void SetAbort()
        {
            Aborted = true;
            OnFailed?.Invoke();
        }

        public ValueTask Upload(string dataForServer)
        {
            // Reset to default
            Failed = false;
            Started = false;
            Aborted = false;
            Progress = 0;

            return JsInteropService.StartFileUpload(Id, dataForServer);
        }

        public ValueTask Abort()
        {
            // Reset to default
            return JsInteropService.AbortFileUpload(Id);
        }
    }
}

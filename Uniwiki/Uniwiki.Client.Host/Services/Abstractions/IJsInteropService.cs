using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Uniwiki.Client.Host.Components.FileUploader;

namespace Uniwiki.Client.Host.Services.Abstractions
{
    public interface IJsInteropService
    {
        ValueTask FocusElementAsync(ElementReference element);
        ValueTask HideCollapse(string elementId);
        ValueTask<bool> NavigationBack();
        ValueTask<string> GetBrowserLanguage();
        ValueTask ScrollIntoView(string elementId);
        ValueTask SetScrollCallback(DotNetObjectReference<ScrollService> netRef);
        ValueTask SetHeightToInitial(ElementReference element);
        ValueTask MyInputInit(ElementReference? fileInput, DotNetObjectReference<InputFileCallbacks> callbacksAsNetRef);
        ValueTask StartFileUpload(in string id, string dataForServer);
        ValueTask AbortFileUpload(in string id);
        ValueTask Download(in string data, in string fileName);
    }
}

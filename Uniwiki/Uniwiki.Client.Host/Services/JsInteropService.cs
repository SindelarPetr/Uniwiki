using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Uniwiki.Client.Host.Components.FileUploader;
using Uniwiki.Client.Host.Services.Abstractions;
using Uniwiki.Shared;

namespace Uniwiki.Client.Host.Services
{
    public class JsInteropService : IJsInteropService
    {
        private readonly IJSRuntime _jsRuntime;

        public JsInteropService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public ValueTask FocusElementAsync(ElementReference element) => 
            _jsRuntime.InvokeVoidAsync("interopJsFunctions.FocusElement", element);
        
        public ValueTask HideCollapse(string elementId) => 
            _jsRuntime.InvokeVoidAsync("interopJsFunctions.HideCollapse", elementId);

        public ValueTask<bool> NavigationBack() => 
            _jsRuntime.InvokeAsync<bool>("interopJsFunctions.GoBack");

        public ValueTask<string> GetBrowserLanguage() => 
            _jsRuntime.InvokeAsync<string>("interopJsFunctions.GetBrowserLocale");
        
        public ValueTask ScrollIntoView(string elementId) => 
            _jsRuntime.InvokeVoidAsync("interopJsFunctions.ScrollIntoView", elementId);

        public ValueTask SetScrollCallback(DotNetObjectReference<ScrollService> netRef) => 
            _jsRuntime.InvokeVoidAsync("interopJsFunctions.SetScrollCallbacks", netRef);

        public ValueTask SetHeightToInitial(ElementReference element) =>
            _jsRuntime.InvokeVoidAsync("interopJsFunctions.SetHeightToInitial", element);

        public ValueTask MyInputInit(ElementReference? fileInput, DotNetObjectReference<InputFileCallbacks> callbacksAsNetRef) => 
            _jsRuntime.InvokeVoidAsync("MyInput.init", fileInput, callbacksAsNetRef);

        public ValueTask StartFileUpload(in string id, string dataForServer) => 
            _jsRuntime.InvokeVoidAsync("MyInput.startUpload", id, dataForServer, ApiRoutes.FileController.UploadFile.BuildRoute());

        public ValueTask AbortFileUpload(in string id) => 
            _jsRuntime.InvokeVoidAsync("MyInput.abortUpload", id);
        
        public ValueTask Download(in string data, in string fileName) =>
            _jsRuntime.InvokeVoidAsync("interopJsFunctions.Download", data, fileName);
    }
}
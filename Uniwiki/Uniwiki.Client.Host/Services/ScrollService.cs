using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Uniwiki.Client.Host.Services.Abstractions;

namespace Uniwiki.Client.Host.Services
{
    public class ScrollService : IScrollService
    {
        private readonly IJsInteropService _jsInteropService;
        public event Func<Task>? ScrolledToEnd;

        private bool _isInitialized;
        private DotNetObjectReference<ScrollService> _thisAsNetRef;

        public ScrollService(IJsInteropService jsInteropService)
        {
            _jsInteropService = jsInteropService;
        }

        public async Task InitializeScroll()
        {
            if(_isInitialized)
            {
                return;
            }

            _thisAsNetRef = DotNetObjectReference.Create(this);
            await _jsInteropService.SetScrollCallback(_thisAsNetRef);

            _isInitialized = true;
        }

        /// <summary>
        /// This should be called from javascript
        /// </summary>
        [JSInvokable(nameof(CallScrolledToEnd))]
        public async Task CallScrolledToEnd()
        {
            if (ScrolledToEnd != null)
            {
                await ScrolledToEnd();
            }
        }
    }
}

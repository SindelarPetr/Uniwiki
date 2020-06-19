using Microsoft.AspNetCore.Components;
using Uniwiki.Client.Host.Services;

namespace Uniwiki.Client.Host.Shared
{
    public class UniwikiComponentBase : ComponentBase
    {
        [Inject] internal TextService TextService { get; set; }

        protected new void StateHasChanged()
        {
            // Make sure, that StateHasChanged is not called during tests (it throws an exception)
            if (!Program.IsTest) 
                base.StateHasChanged();
        }
    }
}
using System;
using Microsoft.AspNetCore.Components;
using Uniwiki.Client.Host.Services;

namespace Uniwiki.Client.Host.Components
{
    public class UniwikiComponentBase : ComponentBase
    {
        [Inject] internal TextService TextService { get; set; } = null!;

        protected new void StateHasChanged()
        {
            // Make sure, that StateHasChanged is not called during tests (it throws an exception)
            if (!Program.IsTest)
            {
                base.StateHasChanged();
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            Console.WriteLine("RERENDERED: " + GetType().Name);
        }
    }
}
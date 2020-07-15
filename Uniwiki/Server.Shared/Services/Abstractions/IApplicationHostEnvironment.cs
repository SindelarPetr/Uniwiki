using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Services.Abstractions
{
    public interface IApplicationHostEnvironment
    {
        bool IsDevelopment();
        bool IsStaging();
        bool IsProduction();
        string Environment { get; }
    }
}

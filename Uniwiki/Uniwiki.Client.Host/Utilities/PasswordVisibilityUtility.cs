using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uniwiki.Client.Host.Utilities
{
    public class PasswordVisibilityUtility
    {
        public bool PasswordVisible { get; private set; }
        public string InputType => PasswordVisible ? "text" : "password";

        public void Toggle() => PasswordVisible = !PasswordVisible;

    }
}

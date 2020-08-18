using System;
using System.Collections.Generic;
using System.Text;
using Uniwiki.Server.Shared.Configuration;

namespace Uniwiki.Server.Application.Configuration
{
    public class UniwikiConfiguration
    {
        public AdministrationConfiguration Administration { get; set; }
            = new AdministrationConfiguration();

        public EmailConfiguration Email { get; set; }
            = new EmailConfiguration();

        public DefaultsConfiguration Defaults { get; set; }
         = new DefaultsConfiguration();
    }
}

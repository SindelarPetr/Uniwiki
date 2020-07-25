using System;
using System.Collections.Generic;
using System.Text;

namespace Uniwiki.Server.Application.Configuration
{
    public class UniwikiConfiguration
    {
        public AdministrationConfiguration Administration { get; set; }
            = new AdministrationConfiguration();

        public EmailConfiguration Email { get; set; }
            = new EmailConfiguration();
    }

    public class EmailConfiguration
    {
        public string SenderAddress { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string DisplayName { get; set; }
    }

    public class AdministrationConfiguration
    {
        public string AccessKey { get; set; }
    }
}

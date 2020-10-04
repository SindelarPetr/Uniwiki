namespace Uniwiki.Server.Shared.Configuration
{
    public class UniwikiConfiguration
    {
        public AdministrationConfiguration Administration { get; set; }
            = new AdministrationConfiguration();

        public EmailConfiguration Email { get; set; }
            = new EmailConfiguration();
    }
}

namespace Uniwiki.Server.Shared.Configuration
{
    public class EmailConfiguration
    {
        public string SenderAddress { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string DisplayName { get; set; }
    }
}

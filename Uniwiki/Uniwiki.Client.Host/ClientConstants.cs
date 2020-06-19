using Uniwiki.Shared;

namespace Uniwiki.Client.Host
{
    public static class ClientConstants
    {
        public static string AppVersionString => typeof(ClientConstants).Assembly.GetName().Version.ToString() + ';' + typeof(Constants).Assembly.GetName().Version.ToString();
    }
}

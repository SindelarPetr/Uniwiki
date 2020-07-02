using Uniwiki.Shared;

namespace Uniwiki.Client.Host
{
    public static class ClientConstants
    {
        public static string AppVersionString => ((long)typeof(ClientConstants).Assembly.GetName().Version.Build + (long)typeof(Constants).Assembly.GetName().Version.Build).ToString();
    }
}

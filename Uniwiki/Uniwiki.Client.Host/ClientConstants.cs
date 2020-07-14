using Uniwiki.Shared;

namespace Uniwiki.Client.Host
{
    public static class ClientConstants
    {
        public static string AppVersionString => //((long)typeof(ClientConstants).Assembly.GetHashCode() + typeof(Constants).Assembly.GetHashCode()).ToString();
            (typeof(ClientConstants).Assembly.GetName().Version.Build + typeof(ClientConstants).Assembly.GetName().Version.Revision
            + (long)typeof(Constants).Assembly.GetName().Version.Build + typeof(Constants).Assembly.GetName().Version.Revision).ToString();
    }
}

using System.Threading.Tasks;
using Uniwiki.Server.Host.Mvc;

namespace Uniwiki.Server.Host
{
    public class Program
    {

        public static async Task Main()
        {
            var uniwiki = new MvcModule();

            await uniwiki.Run();
        }
    }
}

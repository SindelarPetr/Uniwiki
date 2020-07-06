using System.Threading.Tasks;
using Uniwiki.Server.Host.Mvc;

namespace Uniwiki.Server.Host
{
    public class Program
    {

        public static void Main()
        {
            var uniwiki = new MvcModule();

            uniwiki.Run();
        }
    }
}

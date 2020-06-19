using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Uniwiki.Shared.Tests
{
    [TestClass]
    public class UrlEncoding
    {

        [TestMethod]
        public void TryUti()
        {
            var secret = Guid.NewGuid().ToString();
            var email = "\"petr.s+i-nde\"@gmail.com";
            var path = PageRoutes.EmailConfirmedPage.BuildRoute(secret, email);
            var route = "https://www.aaaaaa.cz/" + path;

            Console.WriteLine(route);

            var emailAgain = PageRoutes.EmailConfirmedPage.TryGetEmail(route);

            Console.WriteLine(emailAgain);
        }
    }
}

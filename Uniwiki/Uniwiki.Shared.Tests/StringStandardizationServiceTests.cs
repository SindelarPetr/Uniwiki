using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Uniwiki.Shared.Tests
{
    [TestClass]
    public class StringStandardizationServiceTests
    {
        [TestMethod]
        public void CreateProfileUrlMakesCorrectUrl()
        {
            string name = "Pet  kjncdk 😲 ☹️ 🙁jcn cdccdcd4$$44lkc4r4c$$cc $$ c";
            string surname = "Šind 😲 ☹️ el--- ????scDS::::~~~~~~~~$$$$$$$$$4ář";

            var service = new StringStandardizationService();

            var url = service.CreateUrl(name + surname, s => true);
            Console.WriteLine(url);
        }
    }
}

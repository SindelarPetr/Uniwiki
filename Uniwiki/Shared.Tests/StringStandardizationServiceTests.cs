using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Services;
using System;

namespace Shared.Tests
{
    [TestClass]
    public class StringStandardizationServiceTests
    {
        [TestMethod]
        public void RemovesAccents()
        {
            // Arrange
            string accentString = "ěščřžýáíéňíéůúåü";
            string expectString = "escrzyaienieuuau";
            
            // Act + Assert
            TestCreateUrlMethod(accentString, expectString);
        }

        [TestMethod]
        public void MakesStringLowerCase()
        {
            TestCreateUrlMethod("SOMeText", "sometext");
        }

        [TestMethod]
        public void SubstitutesSpacesCorrectly()
        {
            TestCreateUrlMethod(" \t a\b b\b  c \n de \n", "a-b-c-de");
        }

        [TestMethod]
        public void RemovesNonletters()
        {
            TestCreateUrlMethod("99$$ a&&665531b\b+=ˇ=´´\"'@#....$%^&*()(./,\';][``~|^!  c \n d \n", "ab-c-d");
        }

        [TestMethod]
        public void MakesCorrectUrl()
        {
            string inputString = "😲 ☹️ P🙁531\b+=ˇ=´´\"'@#$...%..^&*(&\t      \b \b  \nE665531 \b+=ˇ=´´\"'@#$%^&*()(./,\';]t[``~|^!  )(./,\';][``~|^4$$4444$$ $$ 😲 ☹️ --- ????::::~~~~~~~~$$$$$r$$$$4";
            string expected = "p-e-t-r";

            TestCreateUrlMethod(inputString, expected);
        }

        private void TestCreateUrlMethod(string input, string expected)
        {
            // Arrange
            var stringStandardizationService = new StringStandardizationService();

            // Act
            var actualString = stringStandardizationService.CreateUrl(input, s => true);

            // Assert
            Assert.AreEqual(expected, actualString);
        }
    }
}

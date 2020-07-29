using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shared.Tests
{
    [TestClass]
    public class FileHelperServiceTests
    {
        [TestMethod] public void Test_GetFileNameAndExtension_WithARegilarName() => Test("file.pdf", "file", ".pdf");
        [TestMethod] public void Test_GetFileNameAndExtension_WithARegilarNameWithTwoExtensions() => Test("file.pdf.docx", "file.pdf", ".docx");
        [TestMethod] public void Test_GetFileNameAndExtension_WithANameWithOneDotAtStart() => Test(".pdf", ".pdf", "");
        [TestMethod] public void Test_GetFileNameAndExtension_WithANameWithTwoDotsAtStart() => Test("..pdf", ".", ".pdf");
        [TestMethod] public void Test_GetFileNameAndExtension_WithANameWithOneDotAtTheEnd() => Test("file.", "file", "");
        [TestMethod] public void Test_GetFileNameAndExtension_WithANameWhichIsJustADot() => Test(".", ".", "");

        private void Test(string fullFileName, string expectedFileName, string expectedExtension)
        {
            // Arrange
            var service = new FileHelperService();

            // Act
            var (fileName, extension) = service.GetFileNameAndExtension(fullFileName);

            // Assert
            Assert.AreEqual(expectedFileName, fileName);
            Assert.AreEqual(expectedExtension, extension);
        }
    }
}

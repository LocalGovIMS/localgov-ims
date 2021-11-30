using BusinessLogic.Classes.Formatters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Classes.Formatters.AccountReference
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FormatTests
    {
        [DataRow("parkingfine", "12345678", "BJ12345678")]
        [DataRow("parkingfine", "1234567", "BJ1234567A")]
        [DataRow("fixedpenaltynotice", "12345678", "FP12345678")]
        [DataRow("fixedpenaltynotice", "1234567", "FP1234567A")]
        [TestMethod]
        public void AccountCodeFormatChecks(string type, string reference, string formattedReference)
        {
            // Arrange
            var formatter = new AccountReferenceFormatter();

            // Act
            var result = formatter.Format(type, reference);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string));
            Assert.AreEqual(result, formattedReference);
        }
    }
}

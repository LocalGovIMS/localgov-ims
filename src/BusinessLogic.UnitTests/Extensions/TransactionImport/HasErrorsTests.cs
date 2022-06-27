using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.TransactionImport
{
    [TestClass]
    public class HasErrorsTests : TestBase
    {
        [TestMethod]
        public void HasErrors_OnATransactionImportWithErrors_ReturnsTrue()
        {
            // Arrange
            var transactionImport = GetTransactionImportWithErrors();

            // Act
            var result = transactionImport.HasErrors();

            // Assert
            result
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public void HasErrors_OnATransactionImportWithoutErrors_ReturnsFalse()
        {
            // Arrange
            var transactionImport = GetTransactionImport();

            // Act
            var result = transactionImport.HasErrors();

            // Assert
            result
                .Should()
                .BeFalse();
        }
    }
}

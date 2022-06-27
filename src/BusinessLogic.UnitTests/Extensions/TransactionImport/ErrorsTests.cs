using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.TransactionImport
{
    [TestClass]
    public class ErrorsTests : TestBase
    {
        [TestMethod]
        public void Errors_OnATransactionImportWithErrors_ReturnsTheExpectedNumberOfErrors()
        {
            // Arrange
            var transactionImport = GetTransactionImportWithErrors();

            // Act
            var result = transactionImport.Errors();

            // Assert
            result.Count()
                .Should()
                .Be(2);
        }



        [TestMethod]
        public void Errors_OnATransactionImportWithoutErrors_ReturnsTheExpectedNumberOfErrors()
        {
            // Arrange
            var transactionImport = GetTransactionImportWithInfo();

            // Act
            var result = transactionImport.Errors();

            // Assert
            result.Count()
                .Should()
                .Be(0);
        }
    }
}

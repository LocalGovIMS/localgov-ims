using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.TransactionImport
{
    [TestClass]
    public class AddErrorTests : TestBase
    {
        [TestMethod]
        public void AddError_OnATransaction_CreatesAnEventLog()
        {
            // Arrange
            var transactionImport = GetTransactionImport();

            // Act
            transactionImport.AddError("An error");

            // Assert
            transactionImport.EventLogs.Count()
                .Should()
                .Be(1);
        }

        [TestMethod]
        public void AddError_OnATransaction_CreatesAnEventLogOfTheCorrectType()
        {
            // Arrange
            var transactionImport = GetTransactionImport();

            // Act
            transactionImport.AddError("An error");

            // Assert
            transactionImport.EventLogs.First().Type
                .Should()
                .Be((byte)TransactionImportEventLogTypeEnum.Error);
        }
    }
}

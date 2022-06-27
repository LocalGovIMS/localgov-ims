using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.TransactionImport
{
    [TestClass]
    public class AddInfoTests : TestBase
    {
        [TestMethod]
        public void AddInfo_OnATransaction_CreatesAnEventLog()
        {
            // Arrange
            var transactionImport = GetTransactionImport();

            // Act
            transactionImport.AddInfo("An error");

            // Assert
            transactionImport.EventLogs.Count()
                .Should()
                .Be(1);
        }

        [TestMethod]
        public void AddInfo_OnATransaction_CreatesAnEventLogOfTheCorrectType()
        {
            // Arrange
            var transactionImport = GetTransactionImport();

            // Act
            transactionImport.AddInfo("An error");

            // Assert
            transactionImport.EventLogs.First().Type
                .Should()
                .Be((byte)TransactionImportEventLogTypeEnum.Info);
        }
    }
}

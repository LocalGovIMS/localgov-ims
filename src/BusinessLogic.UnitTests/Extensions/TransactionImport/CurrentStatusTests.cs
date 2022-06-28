using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Extensions.TransactionImport
{
    [TestClass]
    public class CurrentStatusTests : TestBase
    {
        [TestMethod]
        [DataRow(TransactionImportStatusEnum.Completed)]
        [DataRow(TransactionImportStatusEnum.Failed)]
        [DataRow(TransactionImportStatusEnum.Reverted)]
        public void CurrentStatus_OnATransactionImport_ReturnsTheLastestStatusHistoryStatus(TransactionImportStatusEnum latestStatus)
        {
            // Arrange
            var transactionImport = GetTransactionWithImportStatusHistories(latestStatus);

            // Act
            var result = transactionImport.CurrentStatus();

            // Assert
            result
                .Should()
                .Be(latestStatus);
        }
    }
}

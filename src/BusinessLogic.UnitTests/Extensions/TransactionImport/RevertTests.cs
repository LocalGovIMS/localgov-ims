using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.TransactionImport
{
    [TestClass]
    public class RevertTests : TestBase
    {
        [TestMethod]
        public void Revert_OnATransactionImport_AddsAStatusHistoryEntry()
        {
            // Arrange
            var transactionImport = GetTransactionImport();

            // Act
            transactionImport.Revert(TestUserId);

            // Assert
            transactionImport.StatusHistories.Count()
                .Should()
                .Be(1);
        }

        [TestMethod]
        public void Revert_OnATransactionImport_AddsAStatusHistoryWithAStatusOfReverted()
        {
            // Arrange
            var transactionImport = GetTransactionImport();

            // Act
            transactionImport.Revert(TestUserId);

            // Assert
            transactionImport.StatusHistories.First().StatusId
                .Should()
                .Be((int)TransactionImportStatusEnum.Reverted);
        }
    }
}

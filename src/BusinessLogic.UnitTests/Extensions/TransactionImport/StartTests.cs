using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.TransactionImport
{
    [TestClass]
    public class StartTests : TestBase
    {
        [TestMethod]
        public void Start_OnATransactionImport_AddsAStatusHistoryEntry()
        {
            // Arrange
            var transactionImport = GetTransactionImport();

            // Act
            transactionImport.Start(TestUserId);

            // Assert
            transactionImport.StatusHistories.Count()
                .Should()
                .Be(1);
        }

        [TestMethod]
        public void Start_OnATransactionImport_AddsAStatusHistoryWithAStatusOfInProgress()
        {
            // Arrange
            var transactionImport = GetTransactionImport();

            // Act
            transactionImport.Start(TestUserId);

            // Assert
            transactionImport.StatusHistories.First().StatusId
                .Should()
                .Be((int)TransactionImportStatusEnum.InProgress);
        }
    }
}

using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.TransactionImport
{
    [TestClass]
    public class CompleteTests : TestBase
    {
        private readonly List<string> Errors = new List<string>() { "Error", "Another Error" };
        private readonly List<string> NoErrors = new List<string>();

        [TestMethod]
        public void Complete_OnATransactionImport_AddsAStatusHistoryEntry()
        {
            // Arrange
            var transactionImport = GetTransactionImport();

            // Act
            transactionImport.Complete(Errors, TestUserId);

            // Assert
            transactionImport.StatusHistories.Count()
                .Should()
                .Be(1);
        }

        [TestMethod]
        public void Revert_OnATransactionImportWithErrors_AddsAStatusHistoryWithAStatusOfFailed()
        {
            // Arrange
            var transactionImport = GetTransactionImport();

            // Act
            transactionImport.Complete(Errors, TestUserId);

            // Assert
            transactionImport.StatusHistories.First().StatusId
                .Should()
                .Be((int)TransactionImportStatusEnum.Failed);
        }

        [TestMethod]
        public void Revert_OnATransactionImportWithNoErrors_AddsAStatusHistoryWithAStatusOfCompleted()
        {
            // Arrange
            var transactionImport = GetTransactionImport();

            // Act
            transactionImport.Complete(NoErrors, TestUserId);

            // Assert
            transactionImport.StatusHistories.First().StatusId
                .Should()
                .Be((int)TransactionImportStatusEnum.Completed);
        }
    }
}

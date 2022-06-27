using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.TransactionImport
{
    [TestClass]
    public class InitialiseTests : TestBase
    {
        [TestMethod]
        public void Initialise_OnATransactionImport_AddsAStatusHistoryEntry()
        {
            // Arrange
            var transactionImport = GetTransactionImport();

            // Act
            transactionImport.Initialise(TestUserId);

            // Assert
            transactionImport.StatusHistories.Count()
                .Should()
                .Be(1);
        }

        [TestMethod]
        public void Initialise_OnATransactionImport_AddsAStatusHistoryWithAStatusOfReceived()
        {
            // Arrange
            var transactionImport = GetTransactionImport();

            // Act
            transactionImport.Initialise(TestUserId);

            // Assert
            transactionImport.StatusHistories.First().StatusId
                .Should()
                .Be((int)TransactionImportStatusEnum.Received);
        }
    }
}

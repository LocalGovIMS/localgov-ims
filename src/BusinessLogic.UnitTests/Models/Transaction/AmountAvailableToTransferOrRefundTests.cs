using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class AmountAvailableToTransferOrRefundTests : TestBase
    {
        [TestMethod]
        public void AmountAvailableToTransferOrRefund_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            var transactionLinesTotal = transaction.TransactionLines.Sum(x => x.Amount).ToTwoDecimalPlaces();
            var pendingRefundsTotal = transaction.PendingRefunds.Sum(x => x.Amount).ToTwoDecimalPlaces();
            var processedRefundsTotal = transaction.ProcessedRefunds.Sum(x => x.Amount).ToTwoDecimalPlaces();
            var transfersTotal = transaction.Transfers.Sum(x => x.Amount).ToTwoDecimalPlaces();

            var expectedValue = transactionLinesTotal + pendingRefundsTotal + processedRefundsTotal + transfersTotal;

            // Act
            var value = transaction.AmountAvailableToTransferOrRefund;

            // Assert
            value.Should().Be(expectedValue);
        }
    }
}

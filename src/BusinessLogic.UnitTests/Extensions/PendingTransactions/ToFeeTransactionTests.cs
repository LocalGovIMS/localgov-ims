using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.ProcessedTransaction
{
    [TestClass]
    public class ToFeeTransactionTests
    {
        [TestMethod]
        [DataRow("APspReference", "1.50", "M1")]
        [DataRow("AnotherPspReference", "2.29", "M2")]
        public void ToFeeTransaction_returns_the_expected_result(string pspReference, string fee, string mopCode)
        {
            // Arrange
            var pendingTransactions = GetPendingTransactions();
            var pendingTransaction = pendingTransactions.First();
            var decimalFee = Convert.ToDecimal(fee);

            // Act
            var result = pendingTransactions.ToFeeTransaction(pspReference, decimalFee, mopCode);

            // Assert
            result.TransactionReference.Should().Be($"{pendingTransaction.InternalReference}_{pendingTransactions.Count + 1}");
            result.InternalReference.Should().Be(pendingTransaction.InternalReference);
            result.PspReference.Should().Be(pspReference);
            result.OfficeCode.Should().Be(pendingTransaction.OfficeCode);
            result.EntryDate.Should().BeAfter(pendingTransaction.EntryDate.Value);
            result.TransactionDate.Should().BeAfter(pendingTransaction.TransactionDate.Value);
            result.UserCode.Should().Be(pendingTransaction.UserCode);
            result.FundCode.Should().Be(pendingTransaction.FundCode);
            result.MopCode.Should().Be(mopCode);
            result.Amount.Should().Be(decimalFee);
            result.VatCode.Should().Be(pendingTransaction.VatCode);
            result.VatAmount.Should().Be(0);
            result.VatRate.Should().Be(0);
        }

        private List<Entities.PendingTransaction> GetPendingTransactions()
        {
            return new List<Entities.PendingTransaction>()
            {
                new Entities.PendingTransaction()
                {
                    TransactionReference = "ABC123",
                    InternalReference = "DEF456",
                    OfficeCode = "O1",
                    EntryDate = new DateTime(2021, 1, 2, 12, 30, 55, 123),
                    TransactionDate = new DateTime(2021, 1, 1, 12, 30, 55, 123),
                    AccountReference = "AR12345",
                    UserCode = 1,
                    FundCode = "F1",
                    MopCode = "M1",
                    Amount = 10.00M,
                    VatCode = "V1",
                    VatRate = 0.25F,
                    VatAmount = 2.00M,
                    Narrative = "A narrative"
                }
            };
        }
    }
}

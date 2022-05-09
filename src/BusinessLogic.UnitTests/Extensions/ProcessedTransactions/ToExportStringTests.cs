using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace BusinessLogic.UnitTests.Extensions.ProcessedTransaction
{
    [TestClass]
    public class ToExportStringTests
    {
        [TestMethod]
        [DataRow(",")]
        [DataRow("|")]
        public void ToExportString_returns_the_expected_result(string delimiter)
        {
            // Arrange
            var processedTransaction = GetProcessedTransaction();
            var expectedResult = GetExpectedResult(delimiter);
            // Act
            var result = processedTransaction.ToExportString(delimiter);

            // Assert
            result.Should().Be(expectedResult);
        }

        private Entities.ProcessedTransaction GetProcessedTransaction()
        {
            return new Entities.ProcessedTransaction()
            {
                TransactionReference = "ABC123",
                InternalReference = "DEF456",
                PspReference = "GHI789",
                OfficeCode = "O1",
                EntryDate = new System.DateTime(2021, 1, 2, 12, 30, 55, 123),
                TransactionDate = new System.DateTime(2021, 1, 1, 12, 30, 55, 123),
                AccountReference = "AR12345",
                UserCode = 1,
                FundCode = "F1",
                MopCode = "M1",
                Amount = 10.00M,
                VatCode = "V1",
                VatRate = 0.25F,
                VatAmount = 2.00M,
                Narrative = "A narrative"
            };
        }

        private string GetExpectedResult(string delimiter)
        {
            return string.Format("ABC123{0}DEF456{0}GHI789{0}O1{0}2021-01-02 12:30:55.1230000{0}2021-01-01 12:30:55.1230000{0}AR12345{0}1{0}F1{0}M1{0}10.00{0}V1{0}0.25{0}2.00{0}A narrative", delimiter);
        }
    }
}

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class PayeeNameTests : TestBase
    {
        [TestMethod]
        public void PayeeName_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            // Act
            var value = transaction.PayeeName;

            // Assert
            value.Should().Be(CardHolderName);
        }

        [TestMethod]
        public void PayeeName_returns_an_empty_string_when_there_are_only_CreditNote_transactions()
        {
            // Arrange
            var transaction = GetCreditNoteTransaction();

            // Act
            var value = transaction.PayeeName;

            // Assert
            value.Should().Be(string.Empty);
        }
    }
}
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class CreatedByTests : TestBase
    {
        [TestMethod]
        public void CreatedBy_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            // Act
            var value = transaction.CreatedBy;

            // Assert
            value.Should().Be(Username);
        }

        [TestMethod]
        public void CreatedBy_returns_an_empty_string_when_there_are_only_CreditNote_transactions()
        {
            // Arrange
            var transaction = GetTransactionsWithoutAUser();

            // Act
            var value = transaction.CreatedBy;

            // Assert
            value.Should().Be("System / Customer");
        }
    }
}
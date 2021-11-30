using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class AuthorisatonCodeTests : TestBase
    {
        [TestMethod]
        public void AuthorisatonCode_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            // Act
            var value = transaction.AuthorisationCode;

            // Assert
            value.Should().Be(AuthorisationCode);
        }
    }
}
